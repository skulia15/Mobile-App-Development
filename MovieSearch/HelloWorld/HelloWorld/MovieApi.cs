using DM.MovieApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch
{
    public class MovieApi
    {
        public MovieApi()
        {
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
 
        }
    }
}
