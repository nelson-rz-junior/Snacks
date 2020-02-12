using Snacks.Context;
using Snacks.Models;
using System;

namespace Snacks.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Basket GetBasket(Guid basketId, string userId);
        Guid CreateBasket(ApplicationUser user);
        void AddItemBasket(Snack snack, int quantity, Guid basketId);
        int RemoveBasketItem(Snack snack, Guid basketId);
    }
}
