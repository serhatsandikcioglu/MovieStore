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
    public class MovieRepository : GenericRepository<Movie> , IMovieRepository
    {
        private readonly DbSet<Movie> _dbSet;

        public MovieRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _dbSet = appDbContext.Set<Movie>();
        }
        public Movie GetById(int id)
        {
            return _dbSet.Include(x=>x.Director).Include(x=>x.Actors).FirstOrDefault(x=>x.Id == id);
        }
    }
}
