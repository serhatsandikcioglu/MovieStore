using MovieStore.Data.Entities;
using MovieStore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Interfaces
{
    public interface IActorRepository :  IGenericRepository<Actor>
    {
    }
}
