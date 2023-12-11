using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll(string? sort, int page, int size);
        T GetById(int id);
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
        bool IsExist(int id);
    }
}
