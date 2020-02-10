using Snacks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.ViewModels
{
    public class SnackListViewModel
    {
        public IEnumerable<Snack> Snacks { get; set; }
        public string CurrentCategory { get; set; }
    }
}
