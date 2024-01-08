using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using MovieStore.Data.Entities;
using MovieStore.Data.Interfaces;
using MovieStore.Data.ViewModels;
using MovieStore.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Test
{
    public class UserServiceTest
    {
        [Fact]
        public async Task BuyMovie_Success()
        {
            // Arrange
            var userId = "1";
            var movieId = 1;

            var appUser = new AppUser { Id = int.Parse(userId), PurchasedMovies = new List<Movie>() };
            var movie = new Movie { Id = 1, Name = "Test Movie", Price = 10 };

            var userManagerMock = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(appUser);

            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(repo => repo.GetById(movieId)).Returns(movie);

            var orderRepositoryMock = new Mock<IOrderRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.OrderRepository).Returns(orderRepositoryMock.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<OrderViewModel>(It.IsAny<Order>()))
                .Returns((Order order) => new OrderViewModel { Price = order.Price });

            var userService = new UserService(userManagerMock.Object, mapperMock.Object,orderRepositoryMock.Object, movieRepositoryMock.Object, unitOfWorkMock.Object );

            // Act
            var result = await userService.BuyMovie(userId, movieId);
            var purchasedMovie = appUser.PurchasedMovies.FirstOrDefault(x=>x.Id == movie.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movie.Name, result.MovieName);
            Assert.Equal(movie.Price, result.Price);
            Assert.Equal(movie.Id,purchasedMovie.Id);


        }
    }
}
