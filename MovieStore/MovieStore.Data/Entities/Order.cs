using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
