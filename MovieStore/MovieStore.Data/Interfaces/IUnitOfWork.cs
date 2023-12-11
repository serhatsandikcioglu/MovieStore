using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        IActorRepository ActorRepository { get; }
        IDirectorRepository DirectorRepository { get; }
        IMovieRepository MovieRepository { get; }
        IOrderRepository OrderRepository { get; }
    }
}
