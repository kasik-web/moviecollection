using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MyMovieCollection.Models.ViewModels;
using MovieDetails = MyMovieCollection.Models.MovieDetails;
using MyMovieCollection.Data;


namespace MyMovieCollection.Tmdb
{
    public class SearchEngine
    {
        private readonly AppDbContext _db;

        public SearchEngine(AppDbContext db)
        {
            _db = db;
        }

        public static SearchResultsVM SearchMovieByName(string request)
        {
            SearchResultsVM searchResultVM = new SearchResultsVM();

            searchResultVM.Title = new List<string>();
            searchResultVM.Description = new List<string>();
            searchResultVM.ReleasDate = new List<DateTime>();
            searchResultVM.PosterUrl = new List<string>();
            searchResultVM.Id = new List<int>();
            searchResultVM.ExistId = new List<int>();
            searchResultVM = GetMoviesListByName(request, searchResultVM);
            return searchResultVM;
        }

        public static MovieDetails SearchMovieById(int id)
        {
            MovieDetails movieDetails = new MovieDetails();

            movieDetails = GetMovieDetailsById(id, movieDetails);
            return movieDetails;
        }

        private static SearchResultsVM GetMoviesListByName(string MovieName, SearchResultsVM vm)
        {                        
            MovieDbFactory.RegisterSettings(WC.bearerToken);
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = movieApi.SearchByTitleAsync(MovieName).GetAwaiter().GetResult();
            if (response.Results == null)
            {
                return vm;
            }
            else
            {
                foreach (var item in response.Results)
                {
                    ApiQueryResponse<Movie> movieDetail = movieApi.FindByIdAsync(item.Id).GetAwaiter().GetResult();

                    Movie movie = movieDetail.Item;

                    vm.Id.Add(movie.Id);
                    vm.Title.Add(movie.Title);
                    vm.ReleasDate.Add(movie.ReleaseDate);
                    if (movie.Overview.Length > 190)
                    {
                        vm.Description.Add(movie.Overview.Substring(0, 190) + "...");
                    }
                    else
                    {
                        vm.Description.Add(movie.Overview);
                    }
                    vm.PosterUrl.Add("https://image.tmdb.org/t/p/original/" + movie.PosterPath);
                }
            }
            return vm;
        }

        private static MovieDetails GetMovieDetailsById(int id, MovieDetails movieDetails)
        {            
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiQueryResponse<Movie> movieDetail = movieApi.FindByIdAsync(id).GetAwaiter().GetResult();
            Movie movie = movieDetail.Item;

            movieDetails.MovieId = movie.Id;
            movieDetails.Title = movie.Title;
            movieDetails.ReleasDate = movie.ReleaseDate;
            movieDetails.Description = movie.Overview;
            movieDetails.PosterUrl = "https://image.tmdb.org/t/p/original" + movie.PosterPath;
            movieDetails.BackdropUrl = "https://image.tmdb.org/t/p/original" + movie.BackdropPath;
            movieDetails.OriginalLang = movie.OriginalLanguage;
            movieDetails.imdbId = movie.ImdbId;
            foreach (var item in movie.ProductionCompanies)
            {
                string[] tmp = item.ToString().Split(" (");
                movieDetails.Company += tmp[0] + ", ";
            }

            foreach (var item in movie.ProductionCountries)
            {
                string[] tmp = item.ToString().Split(" (");
                movieDetails.Country += tmp[0] + ", ";
            }
            foreach (var item in movie.Genres)
            {
                string[] tmp = item.ToString().Split(" (");
                movieDetails.Genres += tmp[0] + ", ";
            }

            movieDetails.DurationTime = movie.Runtime;

            movieDetails.TrailerUrl = GetTrailer(id);

            movieDetails = GetActorAndImdbRate(movie.ImdbId, movieDetails);

            return movieDetails;
        }

        private static string GetTrailer(int id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://mdblist.p.rapidapi.com/?tm={id}"),
                Headers =
                {
                    { "X-RapidAPI-Key", WC.RapidKey },
                    { "X-RapidAPI-Host", "mdblist.p.rapidapi.com" },
                },
            };

            using (var response = client.SendAsync(request).GetAwaiter().GetResult())
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                string link = "https://www.youtube.com/embed/";
                if (body.Contains("https://youtube.com/watch?v="))
                {
                    string[] temp = body.Split("https://youtube.com/watch?v=");
                    temp = temp[1].Split("\",");
                    link += temp[0];
                }

                return link;
            }
        }
        private static MovieDetails GetActorAndImdbRate(string ImdbId, MovieDetails movieDetails)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://movie-details1.p.rapidapi.com/imdb_api/movie?id={ImdbId}"),
                Headers =
                    {
                        { "X-RapidAPI-Key", WC.RapidKey },
                        { "X-RapidAPI-Host", "movie-details1.p.rapidapi.com" },
                    },
            };

            using (var response = client.SendAsync(request).GetAwaiter().GetResult())
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                string[] tmp = body.Split("\"rating\":");
                tmp = tmp[1].Split(",");
                movieDetails.ImdbRate = tmp[0];
                tmp = body.Split("\"director_names\":[\"");
                tmp = tmp[1].Split("\"],");
                movieDetails.Director = tmp[0];
                tmp = body.Split("\"actors\":");
                tmp = tmp[1].Split("\"name\":\"");
                string actors = "";
                if (tmp.Length < 5)
                {
                    for (int i = 1; i < tmp.Length; i++)
                    {
                        if (i != 1)
                        {
                            actors += ",";
                        }
                        string[] actor = tmp[i].Split("\"}");
                        actors += actor[0];
                    }
                }
                for (int i = 1; i < 6; i++)
                {
                    if (i != 1)
                    {
                        actors += ", ";
                    }
                    string[] actor = tmp[i].Split("\"}");
                    actors += actor[0];
                }
                movieDetails.Actors = actors;

                return movieDetails;
            }
        }
    }
}
