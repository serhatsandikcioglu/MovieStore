using Microsoft.Extensions.DependencyInjection;
using MovieStore.Data.DataBase;
using MovieStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(AppDbContext appDbContext, IServiceProvider serviceProvider)
        {
            _appDbContext = appDbContext;
            _serviceProvider = serviceProvider;
        }
        private IActorRepository _actorRepository;
        private IDirectorRepository _directorRepository;
        private IMovieRepository _movieRepository;
        private IOrderRepository _orderRepository;
        public IActorRepository ActorRepository
        {
            get
            {
                if (_actorRepository == default(IActorRepository))
                    _actorRepository = _serviceProvider.GetRequiredService<IActorRepository>();
                return _actorRepository;
            }

        }
        public IDirectorRepository DirectorRepository
        {
            get
            {
                if (_directorRepository == default(IDirectorRepository))
                    _directorRepository = _serviceProvider.GetRequiredService<IDirectorRepository>();
                return _directorRepository;
            }

        }
        public IMovieRepository MovieRepository
        {
            get
            {
                if (_movieRepository == default(IMovieRepository))
                    _movieRepository = _serviceProvider.GetRequiredService<IMovieRepository>();
                return _movieRepository;
            }

        }
        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == default(IOrderRepository))
                    _orderRepository = _serviceProvider.GetRequiredService<IOrderRepository>();
                return _orderRepository;
            }

        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }
    }
}
