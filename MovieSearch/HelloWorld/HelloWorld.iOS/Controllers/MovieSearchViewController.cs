using System;
using CoreGraphics;
using UIKit;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using System.Collections.Generic;
using MovieDownload;
using System.Threading;
using System.IO;

namespace MovieSearch.iOS.Controllers
{
    public class MovieSearchViewController : UIViewController
    {
        private const double StartX = 20;
        private const double StartY = 80;
        private const double Height = 50;

		private ImageDownloader imageDownloader = new ImageDownloader(new StorageClient());

		public MovieSearchViewController(IMovieDbSettings settings)
        {
            MovieDbFactory.RegisterSettings(settings);
        }

        public int HttpGet { get; private set; }

        public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.View.BackgroundColor = UIColor.White;
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
			UIActivityIndicatorView loading = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
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
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
			
            var searchButton = UIButton.FromType(UIButtonType.RoundedRect);
            searchButton.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            searchButton.SetTitle("Get movies", UIControlState.Normal);
            searchButton.TouchUpInside += async (sender, args) =>
			{
				loading.StartAnimating();
				searchButton.Enabled = false;
				List<Movie> movies = new List<Movie>();

				searchField.ResignFirstResponder();
				if(searchField.Text != null	&& searchField.Text != "")
				{
					ApiSearchResponse<MovieInfo> titleResponse = await movieApi.SearchByTitleAsync(searchField.Text);
					foreach(MovieInfo m in titleResponse.Results)
					{
						string localPathForImage = imageDownloader.LocalPathForFilename(m.PosterPath);
						await imageDownloader.DownloadImage(m.PosterPath, localPathForImage, CancellationToken.None);
						ApiQueryResponse<MovieCredit> creditResponse = await movieApi.GetCreditsAsync(m.Id);
						List<string> cast = GetCast(creditResponse);
						movies.Add(new Movie()
						{
							Title = m.Title,
							Year = m.ReleaseDate.Year,
							Cast = cast,
							ImageName = localPathForImage
						});
					}
				}
				loading.StopAnimating();
				searchButton.Enabled = true;
				this.NavigationController.PushViewController(new MovieTitleController(movies), true);
            };
            return searchButton;
        }

		private static List<string> GetCast(ApiQueryResponse<MovieCredit> creditResponse)
		{
			List<string> cast = new List<string>();
			foreach (MovieCastMember c in creditResponse.Item.CastMembers)
			{
				cast.Add(c.Name);
			}

			return cast;
		}

		private UILabel PromptLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter words in movie title: "
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
    }
}