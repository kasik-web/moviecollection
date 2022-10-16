
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyMovieCollection.Controllers;
using MyMovieCollection.Models;
using System.Security.Cryptography.X509Certificates;

namespace MyMovieCollection.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<MovieDetails> MovieDetails { get; set; }

        public DbSet<AppUser> AppUser { get; set; }

    }
}
