using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using System.Threading.Tasks;
using System.Threading;
using Android.Views.InputMethods;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
	[Activity(Label = "MovieHub", Theme = "@style/MyTheme")]
	public class TopRatedFragment : Fragment
	{
		MovieController movieController = new MovieController();
		private List<Movie> _movies = new List<Movie>();
		View rootView;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle bundle)
		{

			var rootView = inflater.Inflate(Resource.Layout.TopRated, container, false);
			this.rootView = rootView;

			ListView listView = rootView.FindViewById<ListView>(Resource.Id.topRated);
			listView.Adapter = new MovieListAdapter(this.Activity, _movies);

			listView.ItemClick += (sender, args) =>
			{
				var intent = new Intent(this.Context, typeof(MovieDetailActivity));
				var movie = _movies[args.Position];
				intent.PutExtra("singleMovie", JsonConvert.SerializeObject(movie));
				// Push 
				this.StartActivity(intent);
			};


			return rootView;
		}
		
		public async void GenerateTopRatedAsync()
		{
			ListView listView = rootView.FindViewById<ListView>(Resource.Id.topRated);
			var progbar = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar);
			Activity.RunOnUiThread(() =>
			{
				_movies.Clear();
				listView.Adapter = new MovieListAdapter(this.Activity, _movies);
				progbar.Visibility = ViewStates.Visible;

			});
			try
			{
				_movies = await movieController.GetTopRatedAsync();
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
			}
			if (_movies.Count > 0)
			{
				Activity.RunOnUiThread(() =>
				{
					progbar.Visibility = ViewStates.Gone;
					listView.Adapter = new MovieListAdapter(this.Activity, _movies);

				});
			}
		}
	}
}