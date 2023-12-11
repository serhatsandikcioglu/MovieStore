using MovieStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public Movie Movie { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
