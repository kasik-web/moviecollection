using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.EntityFrameworkCore;
using MyMovieCollection.Data;
using MyMovieCollection.Models;
using MyMovieCollection.Models.ViewModels;
using NuGet.Packaging;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using MovieDetails = MyMovieCollection.Models.MovieDetails;




namespace MyMovieCollection.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;        

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            List<string> GenreList = new List<string>();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);           
            HomeVM homeVM = new HomeVM()
            {
                MovieDetail = new List<MovieDetails>(),
            };
            homeVM.MovieDetail.AddRange(_db.MovieDetails);
            if (claim != null)
            {
                homeVM.MovieDetail = homeVM.MovieDetail.FindAll(m => m.UserId == claim.Value);
            }   

            foreach (var item in homeVM.MovieDetail)
            {              
                    if (item.Genres != null)
                    {
                        GenreList.AddRange(item.Genres?.Split(","));
                        GenreList = GenreList.Distinct().ToList();
                        GenreList.Remove(" ");                        
                    }                                      
            }            
            homeVM.GList = GenreList;
            
            //var user = _db.AppUser.FirstOrDefault(a => a.Id == claim.Value);
                      
            
            
            return View(homeVM);
        }

        [Authorize]
        public IActionResult MovieDetailsView(int id)
        {
            if (id == 0)
            {
                id = IntHolderHome.id;
            }
            MovieDetails movieDetails = _db.MovieDetails.FirstOrDefault(u => u.Id == id);
            return View(movieDetails);
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            MovieDetails movieDetails = _db.MovieDetails.FirstOrDefault(u => u.Id == id);
            return View(movieDetails);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MovieDetails obj, int id)
        {
            IntHolderHome.id = id;
            _db.Update(obj);
            _db.SaveChanges();
            TempData[WC.Success] = "Movie Edit SUCCESSFULLY!";
            return RedirectToAction(nameof(MovieDetailsView));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            MovieDetails movieDetails = _db.MovieDetails.FirstOrDefault(u => u.Id == id);
            _db.MovieDetails.Remove(movieDetails);
            _db.SaveChanges();
            TempData[WC.Success] = "Movie Delete SUCCESSFULLY!";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public static class IntHolderHome
    {
        public static int id = 0;
    }
}
   