namespace MyMovieCollection.Models.ViewModels
{
    public class MovieDetails
    {       
                
        public int Id { get; set; }
        public  string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleasDate { get; set; }
        public string PosterUrl { get; set; }
        public string imdbId { get; set; }
        public string BackdropUrl { get; set; }
        public string OriginalLang { get; set; }
        public string OriginalTitle { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public List<string> Genres { get; set; }
        public int DurationTime { get; set; }
        public string Location { get; set; }
        public string Owner { get; set; }
        public string PurchaseStore { get; set; }
        public DateTime PurchasedDate { get; set; }
        public int PurchasePrice { get; set; }
        public int Quantity { get; set; }
        public bool Seen { get; set; }

    }
}
