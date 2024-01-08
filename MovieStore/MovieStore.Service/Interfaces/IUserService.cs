using MovieStore.Data.DTOs;
using MovieStore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> CreateUser(UserCreateDTO createUserDTO);
        Task<OrderViewModel> BuyMovie(string userId, int movieId);
    }
}
