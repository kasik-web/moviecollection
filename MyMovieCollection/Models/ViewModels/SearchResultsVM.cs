namespace MyMovieCollection.Models.ViewModels
{
    public class SearchResultsVM
    {       
        public List<int> Id { get; set; }
        public  List<string> Title { get; set; }
        public List<string> Description { get; set; }
        public List<DateTime> ReleasDate { get; set; }
        public List<string> PosterUrl { get; set; }

        public List<int> ExistId { get; set; }

    }
}
