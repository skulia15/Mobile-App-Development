using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
	class TabBarController : UITabBarController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.TabBar.BarTintColor = UIColor.FromRGB(24, 24, 24);
			this.TabBar.TintColor = UIColor.FromRGB(186, 157, 9);	

			this.SelectedIndex = 0;
		}
	}
}