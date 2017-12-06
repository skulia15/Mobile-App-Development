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
using Com.Bumptech.Glide;

namespace MovieSearch.Droid
{
	[Activity(Label = "Movie Details", Theme = "@style/MyTheme")]
	public class MovieDetailActivity : Activity
	{
		private Movie _movie;
		private const string POSTER_URL = "http://image.tmdb.org/t/p/original";
		

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			RequestWindowFeature(WindowFeatures.NoTitle);
			// Create application here

			SetContentView(Resource.Layout.MovieDetail);

			// Get the passed JSON string of movies
			var jsonStr = this.Intent.GetStringExtra("singleMovie");
			// Convert from JSON string to a list of movies
			_movie = JsonConvert.DeserializeObject<Movie>(jsonStr);

			this.FindViewById<TextView>(Resource.Id.title).Text = _movie.Title + " (" + _movie.Year + ")";
			this.FindViewById<TextView>(Resource.Id.stats).Text = _movie.Runtime + " min | " + _movie.getStringedGenres();
			this.FindViewById<TextView>(Resource.Id.description).Text = _movie.Description;
			var poster = this.FindViewById<ImageView>(Resource.Id.poster);
			Glide.With(this).Load(POSTER_URL + _movie.ImageName).Into(poster);

			var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
			this.SetActionBar(toolbar);
			this.ActionBar.Title = "About This Movie";
		}
	}
}