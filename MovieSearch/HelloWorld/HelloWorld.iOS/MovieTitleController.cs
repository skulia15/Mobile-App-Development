using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace MovieSearch.iOS
{
	public class MovieTitleController : UITableViewController
	{
		private readonly List<string> _movieTitles;

		public MovieTitleController(List<string> movieTitles)
		{
			this._movieTitles = movieTitles;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Movie list";

			this.TableView.Source = new MovieListDataSource(this._movieTitles);
		}
	}
}