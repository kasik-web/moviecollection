using DM.MovieApi.MovieDb.Movies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyMovieCollection.Data;
using MyMovieCollection.Models;
using MyMovieCollection.Tmdb;
using System.Drawing;
using System.Security.Claims;

namespace MyMovieCollection.Controllers
{
    [Authorize]
    public class AddMovieController : Controller
    {
        private readonly AppDbContext _db;

        public AddMovieController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchResult(string request)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Models.ViewModels.SearchRequestVM searchRequest = new Models.ViewModels.SearchRequestVM();
            searchRequest.Request = request;
            Models.ViewModels.SearchResultsVM results = new Models.ViewModels.SearchResultsVM()
            {
                ExistId = new List<int>(),
            };
            results = SearchEngine.SearchMovieByName(request);

            List<MovieDetails> md = new List<MovieDetails>();
            md.AddRange(_db.MovieDetails);
            md = md.FindAll(m => m.UserId == claim.Value);
            foreach (var item in md)
            {
                results.ExistId.Add(item.MovieId);
            }
            return View(results);
        }

        public IActionResult AddDetail(int id, MovieDetails movieDetails)
        {
            IntHolder.id = id;           
            return View(movieDetails);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PreFillMovieDetails(MovieDetails movieDetails)
        {
            MovieDetails final = new MovieDetails();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);            

            final = movieDetails;
            movieDetails = SearchEngine.SearchMovieById(IntHolder.id);
            movieDetails.Location = final.Location;
            movieDetails.PurchasePrice = final.PurchasePrice;
            movieDetails.PurchasedDate = final.PurchasedDate;
            movieDetails.PurchaseStore = final.PurchaseStore;
            movieDetails.Owner = final.Owner;
            movieDetails.Seen = final.Seen;
            movieDetails.Comments = final.Comments;
            movieDetails.UserId = claim.Value;

            _db.MovieDetails.Add(movieDetails);
            _db.SaveChanges();
            TempData[WC.Success] = "Movie Add SUCCESSFULLY!";
            return RedirectToAction("Index", "Home");
        }
    }

    public static class IntHolder
    {
        public static int id = 0;
    }
}
