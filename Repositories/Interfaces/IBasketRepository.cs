using Snacks.Context;
using Snacks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Basket GetBasket(Guid basketId);
        Guid CreateBasket(ApplicationUser user);
        void AddItemBasket(Snack snack, int quantity, Guid basketId);
        int RemoveBasketItem(Snack snack, Guid basketId);
        List<BasketItem> GetBasketItems(Guid basketId);
    }
}
