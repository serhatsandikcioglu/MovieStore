using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using MovieStore.Data.DTOs;
using MovieStore.Data.Entities;
using MovieStore.Data.Interfaces;
using MovieStore.Data.ViewModels;
using MovieStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Service.Services
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ActorViewModel Add(ActorCreateDTO actorCreateDTO)
        {
            Actor actor = _mapper.Map<Actor>(actorCreateDTO);
            _unitOfWork.ActorRepository.Add(actor);
            _unitOfWork.SaveChanges();
            ActorViewModel actorViewModel = _mapper.Map < ActorViewModel > (actor);
            return actorViewModel;
        }

        public void Delete(int id)
        {
            _unitOfWork.ActorRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public List<ActorViewModel> GetAll(string? sort, int page, int size)
        {
            List<Actor> actors = _unitOfWork.ActorRepository.GetAll(sort, page, size);
            List<ActorViewModel> actorViewModels = _mapper.Map < List<ActorViewModel> > (actors);
            return actorViewModels;
        }

        public ActorViewModel GetById(int id)
        {
            Actor actor = _unitOfWork.ActorRepository.GetById(id);
            ActorViewModel actorViewModel = _mapper.Map<ActorViewModel> (actor);
            return actorViewModel;
        }

        public void Update(int id ,ActorUpdateDTO actorUpdateDTO)
        {
            Actor actor = _mapper.Map<Actor>(actorUpdateDTO);
            actor.Id = id;
            _unitOfWork.ActorRepository.Update(actor);
            _unitOfWork.SaveChanges();
        }
        public void Patch(int id, JsonPatchDocument<Actor> patchDoc)
        {
            Actor actor = _unitOfWork.ActorRepository.GetById (id);
            patchDoc.ApplyTo(actor);
            _unitOfWork.SaveChanges();
        }
        public bool IsExist(int id)
        {
            bool actorExist = _unitOfWork.ActorRepository.IsExist(id);
            return actorExist;
        }
    }
}
