using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Android.Views.InputMethods;

namespace MovieSearch.Droid
{
	[Activity (Label = "MovieHub", MainLauncher = true, Icon = "@drawable/icon", Theme="@style/MyTheme")]
	public class MainActivity : Activity
	{
		private MovieController movieController = new MovieController();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			var movieTitleEditText = this.FindViewById<EditText>(Resource.Id.movieTitleEditText);
			var searchButton = this.FindViewById<Button>(Resource.Id.searchButton);
			var movieTitleTextView = this.FindViewById<TextView>(Resource.Id.titleTextView);

			searchButton.Click += async (object sender, EventArgs e) =>
			{
				var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
				manager.HideSoftInputFromWindow(movieTitleEditText.WindowToken, 0);

				movieTitleTextView.Text = "";
				Movie movie = await movieController.GetSingleMovieAsync(movieTitleEditText.Text);
				if(movie == null)
				{
					movieTitleTextView.Text = "[No movies found]";
				}
				else
				{
					movieTitleTextView.Text = movie.Title;
				}
			};
		}
	}
}


