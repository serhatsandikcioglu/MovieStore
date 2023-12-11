using MovieStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.DTOs
{
    public class ActorCreateDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
