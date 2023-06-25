using CinemaObject;
using System.Collections.Generic;

namespace AdminPage.Models
{
    public class ViewModel
    {
        public Movie movie { get; set; }
        public IEnumerable<Movie> movieList { get; set; }
        public IEnumerable<Movie> mostPopularMovies { get; set; }

    }
}
