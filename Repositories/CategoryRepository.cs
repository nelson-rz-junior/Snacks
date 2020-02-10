using Snacks.Context;
using Snacks.Repositories.Interfaces;
using Snacks.Models;
using System.Collections.Generic;

namespace Snacks.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SnackContext _context;

        public CategoryRepository(SnackContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Categories => _context.Categories;
    }
}
