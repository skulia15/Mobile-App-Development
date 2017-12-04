using System;
using CoreGraphics;
using UIKit;
using System.Collections.Generic;
using MovieDownload;
using System.Threading;
using System.IO;
using MovieSearch.Services;
using DM.MovieApi.MovieDb.Movies;
using System.Threading.Tasks;

namespace MovieSearch.iOS.Controllers
{
    public class MovieSearchViewController : UIViewController
    {
		IMovieConverter converter;
		ImageDownloader imageDownloader;
		private const double StartX = 20;
        private const double StartY = 80;
        private const double Height = 50;

		public MovieSearchViewController(MovieConverter converter, ImageDownloader imageDownloader)
        {
			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
			this.converter = converter;
			this.imageDownloader = imageDownloader;
		}

        public int HttpGet { get; private set; }

        public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.View.BackgroundColor = UIColor.FromRGB(51, 51, 51);
			this.Title = "Movie search";

			UILabel promptLabel = PromptLabel();
			UITextField searchField = SearchField();
			UILabel movieLabel = MovieLabel();
			UIActivityIndicatorView loading = Loading();
			UIButton searchButton = SearchButton(movieLabel, searchField, loading);
			this.View.AddSubviews(new UIView[] { promptLabel, searchField, searchButton, movieLabel, loading });
		}

		private UIActivityIndicatorView Loading()
		{
			UIActivityIndicatorView loading = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			loading.Frame = new CGRect((double)this.View.Bounds.Width / 2 - loading.Frame.Width/2, StartY + 3 * Height, loading.Frame.Width, loading.Frame.Height);
			loading.AutoresizingMask = UIViewAutoresizing.All;
			return loading;
		}

		private UILabel MovieLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(StartX, StartY + 3 * Height, this.View.Bounds.Width - 2 * StartX, Height),
                Text = ""
            };
        }

        private UIButton SearchButton(UILabel movieLabel, UITextField searchField, UIActivityIndicatorView loading)
        {
			var searchButton = UIButton.FromType(UIButtonType.RoundedRect);
			
            searchButton.Frame = new CGRect(this.View.Bounds.Width / 2 - this.View.Bounds.Width / 6, StartY + 2 * Height + 15, this.View.Bounds.Width / 3, Height - 20);
            searchButton.SetTitle("Get movies", UIControlState.Normal);
			searchButton.TintColor = UIColor.FromRGB(31, 31, 31);
			searchButton.BackgroundColor = UIColor.FromRGB(186, 157, 9);
			searchButton.Layer.CornerRadius = 10;
			searchButton.TouchUpInside += async (sender, args) =>
			{
				loading.StartAnimating();
				searchButton.Enabled = false;
				searchField.ResignFirstResponder();
				List<Movie> movies = await converter.GetMoviesByTitleAsync(searchField.Text);
				foreach(Movie m in movies)
				{
					m.ImageName = await getPosterAsync(m.ImageName);
				}
				loading.StopAnimating();
				searchButton.Enabled = true;
				this.NavigationController.PushViewController(new MovieTitleController(movies), true);
            };
            return searchButton;
        }

		private UILabel PromptLabel()
        {
			return new UILabel()
			{
				Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
				Text = "Enter words in movie title: ",
				TextColor = UIColor.White
            };
        }

        private UITextField SearchField()
        {
            return new UITextField()
            {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };
        }

		private async Task<string> getPosterAsync(string posterPath)
		{
			string localPathForImage = imageDownloader.LocalPathForFilename(posterPath);
			if (File.Exists(localPathForImage) == false && localPathForImage != "")
			{
				await imageDownloader.DownloadImage(posterPath, localPathForImage, CancellationToken.None);

			}
			return localPathForImage;
		}
	}
}