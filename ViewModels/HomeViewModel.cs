using Snacks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Snack> FavoriteSnacks { get; set; }
    }
}
