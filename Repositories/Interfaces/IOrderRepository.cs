using Snacks.Models;
using System;

namespace Snacks.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrder(int orderId, Guid basketId);
        void CreateOrder(Basket basket, Order order);
    }
}
