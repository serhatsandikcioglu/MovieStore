using Microsoft.EntityFrameworkCore;
using MovieStore.Data.DataBase;
using MovieStore.Data.Interfaces;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            _dbSet = appDbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            _dbSet.Remove(GetById(id));
        }

        public List<T> GetAll(string? sort, int page, int size)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if (!string.IsNullOrWhiteSpace(sort))
            {
                var sortParts = sort.Split(' ');

                if (sortParts.Length == 2 && (sortParts[1].ToLower() == "asc" || sortParts[1].ToLower() == "desc"))
                {
                    string propertyName = sortParts[0].ToLower();

                    var validProperties = typeof(T).GetProperties().Select(p => p.Name.ToLower());

                    if (validProperties.Contains(propertyName))
                    {
                        query = query.AsQueryable().OrderBy(sort);
                    }
                    else
                    {
                        _dbSet.ToList();
                    }
                }
            }
            int skipCount = (page - 1) * size;
            query = query.Skip(skipCount).Take(size);
            return query.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public bool IsExist(int id)
        {
            bool result = _dbSet.AsEnumerable().Any(a => (int)a.GetType().GetProperty("Id").GetValue(a) == id);
            return result;
        }
    }
}
