using MovieStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public Category Category { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public List<Actor> Actors { get; set; }
        public List<AppUser>? Users { get; set; }
        public decimal Price { get; set; }
        public Movie()
    {
        Actors = new List<Actor>();
    }
    }

}
