using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MovieSearch;
using CoreGraphics;

namespace HelloWorld.iOS.Controllers
{
	class MovieDetailsViewController : UIViewController
	{
		private Movie _movie { get; set; }

		private const double StartX = 5;
		private const double StartY = 80;
		private const double Height = 50;

        public MovieDetailsViewController(Movie movie)
		{
			this._movie = movie;
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.View.BackgroundColor = UIColor.White;
			this.Title = "Movie info";

			UILabel titleLabel = TitleLabel();

            UIImageView imageView = new UIImageView()
            {
                Frame = new CGRect(StartX, StartY + Height + 5, 40, 60),

                Image = UIImage.FromFile(this._movie.ImageName)
            };
            this.View.AddSubviews(new UIView[] { titleLabel, imageView });
		}

		private UILabel TitleLabel()
		{
			return new UILabel()
			{
				Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
				Text = _movie.Title + " (" + _movie.Year + ")",
                Font = UIFont.FromName("ArialMT", 13f),
                TextColor = UIColor.FromRGB(0, 0, 0),
                BackgroundColor = UIColor.Clear
            };
		}
	}
}