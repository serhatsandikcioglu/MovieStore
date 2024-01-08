using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.DTOs;
using MovieStore.Data.ViewModels;
using MovieStore.Service.Interfaces;
using System.Security.Claims;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("{movieId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "user,admin")]
        public async Task<ActionResult> BuyMovie(int movieId )
        {
            string userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orderViewModel = await _userService.BuyMovie(userId,movieId);
            if (orderViewModel == null)
            {
                return BadRequest();
            }
            return StatusCode(201,orderViewModel);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SignUp(UserCreateDTO userCreateDTO)
        {
            var userViewModel = await _userService.CreateUser(userCreateDTO);
            if (userViewModel == null)
            {
                return BadRequest();
            }
            return StatusCode(201, userViewModel);
        }
    }
}
