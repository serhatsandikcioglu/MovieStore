using MovieStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.DTOs
{
    public class OrderCreateDTO
    {
        public int AppUserId { get; set; }
        public int MovieId { get; set; }
    }
}
