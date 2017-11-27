using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.MovieApi;

namespace MovieSearch
{
    public class MovieDbSettings : IMovieDbSettings
    {
        public MovieDbSettings()
        {
        }

        string IMovieDbSettings.ApiKey => "c60e010a9dfbf3d465fc908d6320d1c2";

        string IMovieDbSettings.ApiUrl => "http://api.themoviedb.org/3/";
    }
}
