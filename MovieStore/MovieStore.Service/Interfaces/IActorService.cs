using Microsoft.AspNetCore.JsonPatch;
using MovieStore.Data.DTOs;
using MovieStore.Data.Entities;
using MovieStore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Service.Interfaces
{
    public interface IActorService
    {
        List<ActorViewModel> GetAll(string? sort, int page, int size);
        void Delete(int id);
        void Update(ActorUpdateDTO actorUpdateDTO);
        ActorViewModel Add(ActorCreateDTO actorCreateDTO);
        ActorViewModel GetById(int id);
        void Patch(int id, JsonPatchDocument<Actor> patchDoc);
        bool IsExist(int id);
    }
}
