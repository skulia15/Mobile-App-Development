using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MovieSearch.iOS.Views;

namespace MovieSearch.iOS.Controllers
{
	public class MovieListDataSource : UITableViewSource
	{
		private readonly List<Movie> _movies;

		public readonly NSString movieCellId = new NSString("MovieCell");
		private readonly Action<int> _onSelectedMovie;

		public MovieListDataSource(List<Movie> movies, Action<int> onSelectedMovie)
		{
			this._movies = movies;
			this._onSelectedMovie = onSelectedMovie;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (MovieCell)tableView.DequeueReusableCell((NSString)this.movieCellId);
			if (cell == null)
			{
				cell = new MovieCell(this.movieCellId);
			}
			var movie = this._movies[indexPath.Row];
			cell.UpdateCell(movie);
			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return this._movies.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, true);
			this._onSelectedMovie(indexPath.Row);
		}
	}
}