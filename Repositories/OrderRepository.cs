using Snacks.Context;
using Snacks.Repositories.Interfaces;
using Snacks.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Snacks.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SnackContext _context;

        public OrderRepository(SnackContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetOrders(ApplicationUser user)
        {
            return _context.Orders
                .Where(o => o.UserId == user.Id)
                .OrderByDescending(o => o.CreateDate);
        }

        public Order GetOrder(int orderId, ApplicationUser user)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Snack)
                .Where(o => o.Id == orderId && o.UserId == user.Id)
                .FirstOrDefault();
        }

        public void CreateOrder(Basket basket, Order order)
        {
            order.BasketId = basket.Id;
            order.TotalOrder = basket.Total;
            order.CreateDate = DateTime.Now;
            order.UserId = basket.UserId;
            order.Email = basket.Email;
            order.FirstName = basket.FirstName;
            order.LastName = basket.LastName;

            _context.Orders.Add(order);
            _context.SaveChanges();
            
            foreach (var basketItem in basket.BasketItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    BasketId = basket.Id,
                    SnackId = basketItem.SnackId,
                    BasketItemId = basketItem.Id,
                    SnackName = basketItem.SnackName,
                    Quantity = basketItem.Quantity,
                    UnitPrice = basketItem.UnitPrice,
                    CreateDate = DateTime.Now
                };

                _context.OrderItems.Add(orderItem);
                _context.SaveChanges();
            }
        }
    }
}
