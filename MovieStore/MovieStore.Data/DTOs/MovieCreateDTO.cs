using MovieStore.Data.Entities;
using MovieStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.DTOs
{
    public class MovieCreateDTO
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public Category Category { get; set; }
        public int DirectorId { get; set; }
        public List<int> ActorsId { get; set; }
        public decimal Price { get; set; }
    }
}
