using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;

namespace MovieSearch.iOS.Controllers
{
	class MovieDetailsViewController : UIViewController
	{
		private Movie _movie { get; set; }

		private const double StartX = 5;
		private const double StartY = 60;
		private const double Height = 50;
		private const double ImageStartX = StartX;
		private const double ImageStartY = 160;
		private const double ImageWidth = 80;
		private const double ImageHeight = 120;

		public MovieDetailsViewController(Movie movie)
		{
			this._movie = movie;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.View.BackgroundColor = UIColor.FromRGB(51, 51, 51);
			this.Title = "Movie info";

			UILabel titleLabel = TitleLabel();
			UILabel statsLabel = StatsLabel();
			UITextView descriptionLabel = DescriptionLabel();
			UIImageView imageView = DisplayPoster();

			UIView line = MakeLine();

			this.View.AddSubviews(new UIView[] { titleLabel, statsLabel, descriptionLabel, imageView, line });
		}

		private UIView MakeLine()
		{
			return new UIView()
			{
				BackgroundColor = UIColor.LightGray,
				Frame = new CGRect(0, ImageStartY - 15, this.View.Bounds.Width, 1)
			};
		}

		private UITextView DescriptionLabel()
		{
			return new UITextView()
			{
				Frame = new CGRect(ImageStartX + ImageWidth + 5, ImageStartY - 10, this.View.Bounds.Width - 2 * StartX - ImageWidth, this.View.Bounds.Height - ImageStartX),
				Text = _movie.Description,
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName("ArialMT", 13f),
			};
		}

		// Display Movie stats
		private UILabel StatsLabel()
		{
			string genreList = getStringedGenres();
			return new UILabel()
			{
				Frame = new CGRect(StartX, StartY + 40, this.View.Bounds.Width - 2 * StartX, Height),
				Font = UIFont.FromName("ArialMT", 15f),
				TextColor = UIColor.White,
				Text = _movie.Runtime + " min | " + getStringedGenres()
			};
		}

		// Display Poster
		private UIImageView DisplayPoster()
		{
			return new UIImageView()
			{
				Frame = new CGRect(ImageStartX, ImageStartY, ImageWidth, ImageHeight),
				Image = UIImage.FromFile(this._movie.ImageName)
			};
		}

		// Diplay title
		private UILabel TitleLabel()
		{
			return new UILabel()
			{
				Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
				Text = _movie.Title + " (" + _movie.Year + ")",
				Font = UIFont.FromName("ArialMT", 18f),
				TextColor = UIColor.White,
                BackgroundColor = UIColor.Clear
            };
		}

		private string getStringedGenres()
		{
			string stringedGenres = "";
			foreach(string g in _movie.Genres)
			{
				if(stringedGenres != "")
				{
					stringedGenres += ", ";
				}
				stringedGenres += g;
			}
			return stringedGenres;
		}

	}
}