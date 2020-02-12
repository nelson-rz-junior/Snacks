using Snacks.Context;
using Snacks.Models;
using System;
using System.Collections.Generic;

namespace Snacks.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders(ApplicationUser user);
        Order GetOrder(int orderId, ApplicationUser user);
        void CreateOrder(Basket basket, Order order);
    }
}
