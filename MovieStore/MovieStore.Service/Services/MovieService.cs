using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MovieStore.Data.DTOs;
using MovieStore.Data.Entities;
using MovieStore.Data.Interfaces;
using MovieStore.Data.ViewModels;
using MovieStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Service.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public MovieViewModel Add(MovieCreateDTO movieCreateDTO)
        {
            Movie movie = _mapper.Map<Movie>(movieCreateDTO);
            foreach (var actorId in movieCreateDTO.ActorsId)
            {
               Actor actor = _unitOfWork.ActorRepository.GetById(actorId);
                if (actor != null)
                {
                movie.Actors.Add(actor);
                }
            }
            _unitOfWork.MovieRepository.Add(movie);
            _unitOfWork.SaveChanges();
            MovieViewModel movieViewModel = _mapper.Map<MovieViewModel>(movie);
            return movieViewModel;
        }

        public void Delete(int id)
        {
            _unitOfWork.MovieRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public List<MovieViewModel> GetAll(string? sort, int page, int size)
        {
            List<Movie> movies = _unitOfWork.MovieRepository.GetAll(sort, page, size);
            List<MovieViewModel> movieViewModels = _mapper.Map<List<MovieViewModel>>(movies);
            return movieViewModels;
        }

        public MovieViewModel GetById(int id)
        {
            Movie movie = _unitOfWork.MovieRepository.GetById(id);
            MovieViewModel movieViewModel = _mapper.Map<MovieViewModel>(movie);
            return movieViewModel;
        }

        public void Update(int id ,MovieUpdateDTO movieUpdateDTO)
        {
            Movie movie = _unitOfWork.MovieRepository.GetById(id);
            _mapper.Map(movieUpdateDTO, movie);
            _unitOfWork.MovieRepository.Update(movie);
            _unitOfWork.SaveChanges();
        }
        public void Patch(int id, JsonPatchDocument<Movie> patchDoc)
        {
            Movie movie = _unitOfWork.MovieRepository.GetById(id);
            patchDoc.ApplyTo(movie);
            _unitOfWork.SaveChanges();
        }
        public bool IsExist(int id)
        {
            bool movieExist = _unitOfWork.MovieRepository.IsExist(id);
            return movieExist;
        }
    }
}
