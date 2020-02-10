using Snacks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.Repositories.Interfaces
{
    public interface ISnackRepository
    {
        IEnumerable<Snack> Snacks { get; }
        IEnumerable<Snack> FavoriteSnacks { get; }
        Snack GetSnackById(int snackId);
        IEnumerable<Snack> GetSnacks(string searchTerm = null, string categoryName = null);
    }
}
