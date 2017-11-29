using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using HelloWorld.iOS.Controllers;

namespace MovieSearch.iOS.Controllers
{
	public class MovieTitleController : UITableViewController
	{
		private readonly List<Movie> _movies;


		public MovieTitleController(List<Movie> movies)
		{
			this._movies = movies;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Movie list";

			this.TableView.Source = new MovieListDataSource(this._movies, _onSelectedMovie);
		}

		private void _onSelectedMovie(int row)
		{
			this.NavigationController.PushViewController(new MovieDetailsViewController(_movies[row]), true);
		}
	}
}