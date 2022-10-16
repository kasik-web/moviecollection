using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyMovieCollection.Models.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace MyMovieCollection.Models
{
    public class MovieDetails
    { 
        [Key]
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleasDate { get; set; }
        public string PosterUrl { get; set; }
        public string imdbId { get; set; }
        public string BackdropUrl { get; set; }
        public string OriginalLang { get; set; }
        public string OriginalTitle { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Genres { get; set; }        
        public int DurationTime { get; set; }
        public string TrailerUrl { get; set; }
        public string Actors { get; set; }
        public string ImdbRate { get; set; }
        public string Director { get; set; }

        //personal data

        public string Location { get; set; }
        public string Owner { get; set; }
        public string PurchaseStore { get; set; }
        public DateTime PurchasedDate { get; set; }
        public int PurchasePrice { get; set; } = 0;
        public int Quantity { get; set; } = 1;
        public bool Seen { get; set; } = false;
        public string Comments { get; set; }

        public string UserId { get; set; }
    }
    

}

