using Microsoft.AspNetCore.Identity;
using MovieStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Entities
{
    public class AppUser :IdentityUser<int>
    {
        public List<Movie> PurchasedMovies { get; set; }
        public List<Category> FavoriteCategories { get; set; }
        public decimal Balance { get; set; }
    }
}
