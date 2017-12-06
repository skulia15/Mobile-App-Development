using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views.InputMethods;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;
using System.Collections.Generic;
using static Android.Support.V4.View.ViewPager;

namespace MovieSearch.Droid
{
	[Activity (Label = "MovieHub", MainLauncher = true, Icon = "@drawable/Icon", Theme="@style/MyTheme.Toolbar")]
	public class MainActivity : FragmentActivity
	{
		public static List<Movie> Movies { get; set; }
		private MovieController movieController = new MovieController();
		private Fragment[] fragments;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			RequestWindowFeature(WindowFeatures.NoTitle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			fragments = new Fragment[]
			{
				new TitleInputFragment(Movies),
				new TopRatedFragment()
			};

			var titles = CharSequence.ArrayFromStringArray(new[] { "Movies", "Top Rated" });

			var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
			viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);
			var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
			tabLayout.SetupWithViewPager(viewPager);


			viewPager.PageSelected += ViewPager_PageSelected;
			// Get our button from the layout resource,
			// and attach an event to it
			var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
			this.SetActionBar(toolbar);
			this.ActionBar.Title = "Movie Search";
		}

		private void ViewPager_PageSelected(object sender, PageSelectedEventArgs e)
		{
			if (e.Position != 0)
			{
				TitleInputFragment fragment = (TitleInputFragment)fragments[0];
				fragment.HideKeyboard();
			}

			if(e.Position == 1)
			{
				TopRatedFragment fragment = (TopRatedFragment)fragments[e.Position];
				fragment.GenerateTopRatedAsync();
			}

		}
	}
}


