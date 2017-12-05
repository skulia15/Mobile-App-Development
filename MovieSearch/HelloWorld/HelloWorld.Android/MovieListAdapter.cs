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
using Com.Bumptech.Glide;
using MovieDownload;
using Com.Bumptech.Glide.Load.Model;

namespace MovieSearch.Droid
{
	class MovieListAdapter : BaseAdapter<Movie>
	{
		private readonly Activity _context;
		private readonly List<Movie> _movieList;
		private const string POSTER_URL = "http://image.tmdb.org/t/p/original";



		public MovieListAdapter(Activity context, List<Movie> movieList)
		{
			this._context = context;
			this._movieList = movieList;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView; // Reuse an existing view, if available

			if (view == null)
			{
				view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);
			}

			var movie = this._movieList[position];


			view.FindViewById<TextView>(Resource.Id.title).Text = movie.Title + " (" + movie.Year + ")";

			string castMembers = movie.CastToString();

			view.FindViewById<TextView>(Resource.Id.cast).Text = castMembers;
			var resourceId = this._context.Resources.GetIdentifier(movie.ImageName, "drawable", this._context.PackageName);
			var poster = view.FindViewById<ImageView>(Resource.Id.poster);

			Glide.With(_context).Load(POSTER_URL + movie.ImageName).Into(poster);

			return view;
		}

		//Fill in cound here, currently 0
		public override int Count => this._movieList.Count;

		public override Movie this[int position] => this._movieList[position];
	}
}