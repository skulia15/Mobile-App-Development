using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.Threading;
using MovieDownload;
using MovieSearch.Services;
using CoreGraphics;

namespace MovieSearch.iOS.Controllers
{
	public class TopRatedController : UITableViewController
	{
		IMovieConverter converter;
		List<Movie> _movies = new List<Movie>();
		UIActivityIndicatorView loading;
		bool reload = true;
		public TopRatedController(IMovieConverter converter)
		{
			this.converter = converter;
			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Favorites, 1);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Top Rated";
			this.View.BackgroundColor = UIColor.FromRGB(51, 51, 51);
			// Add loading icon to subview
			Loading();
			this.View.AddSubviews(new UIView[] { loading });
        }

		public override async void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			this.ParentViewController.TabBarController.ViewControllerSelected += (sender, err) =>
			{
				if (ParentViewController.TabBarController.SelectedIndex != 1)
				{
					reload = true;
				}
			};
			if (reload)
			{
				loading.StartAnimating();

				_movies = await converter.GetTopRatedMoviesAsync();

				loading.StopAnimating();

				this.TableView.Source = new MovieListDataSource(_movies, _onSelectedMovie);
				this.TableView.ReloadData();
				reload = false;
			}
		}

		private void Loading()
		{
			loading = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			loading.Frame = new CGRect(this.View.Bounds.Width / 2 - loading.Frame.Width / 2, 5, loading.Frame.Width, loading.Frame.Height);
			loading.AutoresizingMask = UIViewAutoresizing.All;
		}

		private void _onSelectedMovie(int row)
		{
			this.NavigationController.PushViewController(new MovieDetailsViewController(_movies[row]), true);
		}
	}
}