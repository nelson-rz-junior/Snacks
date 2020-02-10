using Microsoft.EntityFrameworkCore;
using Snacks.Context;
using Snacks.Repositories.Interfaces;
using Snacks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snacks.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly SnackContext _context;

        public BasketRepository(SnackContext context)
        {
            _context = context;
        }

        public Basket GetBasket(Guid basketId)
        {
            return _context.Baskets
                .Include(i => i.BasketItems)
                .ThenInclude(i => i.Snack)
                .Where(b => b.Id == basketId)
                .FirstOrDefault();
        }

        public Guid CreateBasket(ApplicationUser user)
        {
            var basket = new Basket
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Email = user.Email,
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            _context.Baskets.Add(basket);
            _context.SaveChanges();

            return basket.Id;
        }

        public void AddItemBasket(Snack snack, int quantity, Guid basketId)
        {
            var basketItem = _context.BasketItems
                .FirstOrDefault(s => s.BasketId == basketId && s.SnackId == snack.Id);

            if (basketItem == null)
            {
                basketItem = new BasketItem
                {
                    BasketId = basketId,
                    SnackId = snack.Id,
                    Quantity = quantity,
                    SnackName = snack.Name,
                    UnitPrice = snack.Price,
                    CreateDate = DateTime.Now
                };

                _context.BasketItems.Add(basketItem);
            }
            else
            {
                basketItem.UpdateDate = DateTime.Now;
                basketItem.Quantity++;
            }

            _context.SaveChanges();

            UpdateTotalBasket(basketId);
        }

        public int RemoveBasketItem(Snack snack, Guid basketId)
        {
            int quantity = 0;

            var basketItem = _context.BasketItems
                .FirstOrDefault(s => s.BasketId == basketId && s.SnackId == snack.Id);

            if (basketItem != null)
            {
                if (basketItem.Quantity > 1)
                {
                    basketItem.UpdateDate = DateTime.Now;
                    basketItem.Quantity--;

                    quantity = basketItem.Quantity;
                }
                else
                {
                    _context.BasketItems.Remove(basketItem);
                }

                _context.SaveChanges();

                UpdateTotalBasket(basketId);
            }

            return quantity;
        }

        public List<BasketItem> GetBasketItems(Guid basketId)
        {
            return _context.BasketItems
                .Include(i => i.Snack)
                .Where(b => b.BasketId == basketId)
                .ToList();
        }

        public decimal GetTotalBasket(Guid basketId)
        {
            return _context.BasketItems
                .Where(b => b.BasketId == basketId)
                .Select(s => s.UnitPrice * s.Quantity)
                .Sum();
        }

        private void UpdateTotalBasket(Guid basketId)
        {
            var basket = _context.Baskets.Where(b => b.Id == basketId)
                .FirstOrDefault();

            if (basket != null)
            {
                basket.Total = GetTotalBasket(basketId);
                basket.UpdateDate = DateTime.Now;

                _context.Baskets.Add(basket);
                _context.Entry(basket).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
