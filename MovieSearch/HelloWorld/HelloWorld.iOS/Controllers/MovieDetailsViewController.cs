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

		private const double StartX = 20;
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

			UILabel detailLabel = DetailLabel();
			this.View.AddSubviews(new UIView[] { detailLabel });
		}

		private UILabel DetailLabel()
		{
			return new UILabel()
			{
				Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
				Text = _movie.Title
			};
		}
	}
}