using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Android.Views.InputMethods;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
	[Activity (Label = "MovieHub", MainLauncher = true, Icon = "@drawable/Icon", Theme="@style/MyTheme")]
	public class MainActivity : Activity
	{
		public static List<Movie> Movies { get; set; }
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
			var loadingIcon = this.FindViewById<ProgressBar>(Resource.Id.progressBar);
			loadingIcon.Visibility = ViewStates.Invisible;

			// On search button clicked
			searchButton.Click += async (object sender, EventArgs e) =>
			{
				// Create a manager that handles system services
				var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
				// Hide keyboard
				manager.HideSoftInputFromWindow(movieTitleEditText.WindowToken, 0);

				if(movieTitleEditText.Text != "" && movieTitleEditText != null)
				{
					searchButton.Enabled = false;
					loadingIcon.Visibility = ViewStates.Visible;
					var intent = new Intent(this, typeof(MovieListActivity));

					// Search for movies by title and store them in a list of movies
					Movies = await movieController.GetMoviesByTitleAsync(movieTitleEditText.Text);
					// Convert list of movies to list of movie title strings 
					if (Movies.Count > 0)
					{
						intent.PutExtra("movieList", JsonConvert.SerializeObject(Movies));
					}
					loadingIcon.Visibility = ViewStates.Invisible;
					searchButton.Enabled = true;
					// Push 
					this.StartActivity(intent);
				}
			};
		}
	}
}


