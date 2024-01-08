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
    public interface IOrderService
    {
        List<OrderViewModel> GetAll(string? sort, int page, int size);
        void Delete(int id);
        void Update(int id, OrderUpdateDTO orderUpdateDTO);
        OrderViewModel Add(OrderCreateDTO orderCreateDTO);
        OrderViewModel GetById(int id);
        void Patch(int id, JsonPatchDocument<Order> patchDoc);
        bool IsExist(int id);
    }
}
