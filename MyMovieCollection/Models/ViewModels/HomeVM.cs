using MyMovieCollection.Models.ViewModels;
using System.Collections;

namespace MyMovieCollection.Models
{
    public class HomeVM
    {
        public List<MovieDetails>? MovieDetail { get; set; }

        public List<string>? GList { get; set; }        

    }
}
