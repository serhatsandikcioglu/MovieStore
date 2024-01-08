using AutoMapper;
using MovieStore.Data.DTOs;
using MovieStore.Data.Entities;
using MovieStore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Service.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Actor, ActorViewModel>().ReverseMap();
            CreateMap<Actor, ActorCreateDTO>().ReverseMap();
            CreateMap<Actor, ActorUpdateDTO>().ReverseMap();

            CreateMap<Director, DirectorViewModel>().ReverseMap();
            CreateMap<Director, DirectorCreateDTO>().ReverseMap();
            CreateMap<Director, DirectorUpdateDTO>().ReverseMap();

            CreateMap<Movie, MovieViewModel>().ReverseMap();
            CreateMap<Movie, MovieCreateDTO>().ReverseMap();
            CreateMap<Movie, MovieUpdateDTO>().ReverseMap();

            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();

            CreateMap<AppUser, UserViewModel>().ReverseMap();
            CreateMap<AppUser, UserCreateDTO>().ReverseMap();

        }
    }
}
