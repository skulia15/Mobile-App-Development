using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;

namespace MovieSearch.iOS.Views
{
	public class MovieCell : UITableViewCell
	{
		private const double ImageHeight = 55;
		private UIImageView _imageView;
		private UILabel _headingLabel;
		private UILabel _subheadingLabel;


		public MovieCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
		{
			this.SelectionStyle = UITableViewCellSelectionStyle.Gray;
			this.BackgroundColor = UIColor.FromRGB(51, 51, 51);

			this._imageView = new UIImageView()
			{
				Frame = new CGRect(5, 5, ImageHeight, ImageHeight),
			};

			this._headingLabel = new UILabel()
			{
				Frame = new CGRect(ImageHeight + 15, 10, this.ContentView.Bounds.Width - ImageHeight - 50, 25),
				Font = UIFont.FromName("ArialMT", 15f),
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear
			};

			this._subheadingLabel = new UILabel()
			{
				Frame = new CGRect(ImageHeight + 15, 30, this.ContentView.Bounds.Width - ImageHeight - 50, 20),
				Font = UIFont.FromName("ArialMT", 10f),
				TextColor = UIColor.LightGray,
				BackgroundColor = UIColor.Clear
			};
			this.ContentView.AddSubviews(new UIView[] { this._imageView, this._headingLabel, this._subheadingLabel });

			this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			
		}

		public void UpdateCell(Movie movie)
		{
			if (movie.ImageName != "")
			{
				this._imageView.Image = UIImage.FromFile(movie.ImageName);
			}
			this._headingLabel.Text = movie.Title + " (" + movie.Year + ')';
			string castMembers = "";
			for(int i = 0; i < movie.Cast.Count && i < 3; i++)
			{
				if (i != 0)
				{
					castMembers += ", ";
				}
				castMembers += movie.Cast[i];
			}
			this._subheadingLabel.Text = castMembers;
		}
	}
}