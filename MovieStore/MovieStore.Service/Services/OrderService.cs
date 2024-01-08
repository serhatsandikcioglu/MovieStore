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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public OrderViewModel Add(OrderCreateDTO orderCreateDTO)
        {
            Order order = _mapper.Map<Order>(orderCreateDTO);
            _unitOfWork.OrderRepository.Add(order);
            _unitOfWork.SaveChanges();
            OrderViewModel orderViewModel = _mapper.Map<OrderViewModel>(order);
            return orderViewModel;
        }

        public void Delete(int id)
        {
            _unitOfWork.OrderRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public List<OrderViewModel> GetAll(string? sort, int page, int size)
        {
            List<Order> orders = _unitOfWork.OrderRepository.GetAll(sort, page, size);
            List<OrderViewModel> orderViewModels = _mapper.Map<List<OrderViewModel>>(orders);
            return orderViewModels;
        }

        public OrderViewModel GetById(int id)
        {
            Order order = _unitOfWork.OrderRepository.GetById(id);
            OrderViewModel orderViewModel = _mapper.Map<OrderViewModel>(order);
            return orderViewModel;
        }

        public void Update(int id ,OrderUpdateDTO orderUpdateDTO)
        {
            Order order = _unitOfWork.OrderRepository.GetById(id);
            _mapper.Map(orderUpdateDTO, order);
            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.SaveChanges();
        }
        public void Patch(int id, JsonPatchDocument<Order> patchDoc)
        {
            Order order = _unitOfWork.OrderRepository.GetById(id);
            patchDoc.ApplyTo(order);
            _unitOfWork.SaveChanges();
        }
        public bool IsExist(int id)
        {
            bool orderExist = _unitOfWork.OrderRepository.IsExist(id);
            return orderExist;
        }
    }
}
