using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace MovieSearch.iOS
{
	public class MovieListDataSource : UITableViewSource
	{
		private readonly List<string> _movieTitles;

		public readonly NSString movieTitleCellId = new NSString("MovieTitleCell");

		public MovieListDataSource(List<string> movieTitles)
		{
			this._movieTitles = movieTitles;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell((NSString)this.movieTitleCellId);
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, this.movieTitleCellId);
			}
			cell.TextLabel.Text = this._movieTitles[indexPath.Row];
			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return this._movieTitles.Count;
		}
	}
}