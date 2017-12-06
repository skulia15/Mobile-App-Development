using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
	[Activity(Label = "Movie Search", Theme = "@style/MyTheme")]
	public class MovieListActivity : Activity
	{
		private List<Movie> _movieList;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			RequestWindowFeature(WindowFeatures.NoTitle);
			SetContentView(Resource.Layout.MovieList);
			// Create application here

			var listView = FindViewById<ListView>(Resource.Id.topRated);
			// Get the passed JSON string of movies
			var jsonStr = this.Intent.GetStringExtra("movieList");
			// Convert from JSON string to a list of movies
			if(jsonStr != null && jsonStr != "")
			{
				_movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);
			}

			listView.ItemClick += (sender, args) =>
			{
				var intent = new Intent(this, typeof(MovieDetailActivity));
				var movie = _movieList[args.Position];
				intent.PutExtra("singleMovie", JsonConvert.SerializeObject(movie));
				// Push 
				this.StartActivity(intent);
			};
			// Get our button from the layout resource,
			// and attach an event to it
			var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
			this.SetActionBar(toolbar);
			this.ActionBar.Title = "Search Results";

			listView.Adapter = new MovieListAdapter(this, _movieList);
		}
	}
		
}