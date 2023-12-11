using Microsoft.EntityFrameworkCore;
using MovieStore.Data.DataBase;
using MovieStore.Data.Entities;
using MovieStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Repositories
{
    public class ActorRepository :GenericRepository<Actor>, IActorRepository
    {
        private readonly DbSet<Actor> _dbSet;

        public ActorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _dbSet = appDbContext.Set<Actor>();
        }
    }
}
