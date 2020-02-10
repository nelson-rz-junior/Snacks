using Snacks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.ViewModels
{
    public class BasketViewModel
    {
        public decimal Total { get; set; }

        public List<BasketItem> BasketItems { get; set; }
        
        public BasketViewModel()
        {
            BasketItems = new List<BasketItem>();
        }
    }
}
