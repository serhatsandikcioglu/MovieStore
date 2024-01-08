using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IOrderRepository orderRepository, IMovieRepository movieRepository, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderViewModel> BuyMovie(string userId , int movieId )
        {
            var appUser = await _userManager.FindByIdAsync(userId);
            Movie movie = _movieRepository.GetById(movieId);
            if (appUser == null || movie == null)
            {
                return null;
            }
            Order order = new Order();
            order.AppUserId = int.Parse(userId);
            order.MovieId = movieId;
            order.Price = movie.Price;
            order.CreatedDate = DateTime.UtcNow;
            if (appUser.PurchasedMovies == null)
            {
                List<Movie> movieList = new List<Movie>();
                appUser.PurchasedMovies = movieList;
            }
            appUser.PurchasedMovies.Add(movie);
            _orderRepository.Add(order);
            _unitOfWork.SaveChanges();
            OrderViewModel orderViewModel = _mapper.Map<OrderViewModel>(order);
            orderViewModel.MovieName = movie.Name;
            return orderViewModel;
        }

        public async Task<UserViewModel> CreateUser(UserCreateDTO createUserDTO)
        {
            AppUser appUser = _mapper.Map<AppUser>(createUserDTO);
            var result = await _userManager.CreateAsync(appUser, createUserDTO.Password);
            await _userManager.AddToRoleAsync(appUser,"user");
            var userViewModel = _mapper.Map<UserViewModel>(appUser);

            return userViewModel;
        }
    }
}
