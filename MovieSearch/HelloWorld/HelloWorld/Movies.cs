using System;
using System.Collections.Generic;

namespace MovieSearch
{
    public class Movies
    {
        private List<string> _movieTitles;

        public Movies()
        {
            this._movieTitles = new List<string>();
        }
		public List<string> MovieTitles => this._movieTitles;
	}
}