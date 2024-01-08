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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Service.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DirectorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public DirectorViewModel Add(DirectorCreateDTO directorCreateDTO)
        {
            Director director = _mapper.Map<Director>(directorCreateDTO);
            _unitOfWork.DirectorRepository.Add(director);
            _unitOfWork.SaveChanges();
            DirectorViewModel directorViewModel = _mapper.Map<DirectorViewModel>(director);
            return directorViewModel;
        }

        public void Delete(int id)
        {
            _unitOfWork.DirectorRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public List<DirectorViewModel> GetAll(string? sort, int page, int size)
        {
            List<Director> directors = _unitOfWork.DirectorRepository.GetAll(sort, page, size);
            List<DirectorViewModel> directorViewModels = _mapper.Map<List<DirectorViewModel>>(directors);
            return directorViewModels;
        }

        public DirectorViewModel GetById(int id)
        {
            Director director = _unitOfWork.DirectorRepository.GetById(id);
            DirectorViewModel directorViewModel = _mapper.Map<DirectorViewModel>(director);
            return directorViewModel;
        }

        public void Update(int id ,DirectorUpdateDTO directorUpdateDTO)
        {
            Director director = _unitOfWork.DirectorRepository.GetById(id);
            _mapper.Map(directorUpdateDTO, director);
            _unitOfWork.DirectorRepository.Update(director);
            _unitOfWork.SaveChanges();
        }
        public void Patch(int id, JsonPatchDocument<Director> patchDoc)
        {
            Director director = _unitOfWork.DirectorRepository.GetById(id);
            patchDoc.ApplyTo(director);
            _unitOfWork.SaveChanges();
        }
        public bool IsExist(int id)
        {
            bool directorExist = _unitOfWork.DirectorRepository.IsExist(id);
            return directorExist;
        }
    }
}
