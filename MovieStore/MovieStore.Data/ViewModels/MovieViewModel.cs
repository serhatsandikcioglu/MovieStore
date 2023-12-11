using MovieStore.Data.Entities;
using MovieStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public Category Category { get; set; }
        public Director Director { get; set; }
        public List<Actor> Actors { get; set; }
        public decimal Price { get; set; }
    }
}
