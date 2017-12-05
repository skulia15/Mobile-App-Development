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
	[Activity(Label = "MovieListActivity", Theme = "@style/MyTheme")]
	public class MovieListActivity : ListActivity
	{
		private List<Movie> _movieList;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			// Create application here

			// Get the passed JSON string of movies
			var jsonStr = this.Intent.GetStringExtra("movieList");
			// Convert from JSON string to a list of movies
			_movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);

			this.ListView.ItemClick += (sender, args) =>
			{
				var intent = new Intent(this, typeof(MovieDetailActivity));
				var movie = _movieList[args.Position];
				intent.PutExtra("singleMovie", JsonConvert.SerializeObject(movie));
				// Push 
				this.StartActivity(intent);
			};
			this.ListAdapter = new MovieListAdapter(this, _movieList);
		}
	}
		
}