using Microsoft.EntityFrameworkCore;
using Snacks.Context;
using Snacks.Repositories.Interfaces;
using Snacks.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Snacks.Repositories
{
    public class SnackRepository : ISnackRepository
    {
        private readonly SnackContext _context;

        public SnackRepository(SnackContext context)
        {
            _context = context;
        }

        public IEnumerable<Snack> Snacks => _context.Snacks.Include(s => s.Category);

        public IEnumerable<Snack> FavoriteSnacks => _context.Snacks.Include(s => s.Category)
            .Where(s => s.IsFavoriteSnack);

        public Snack GetSnackById(int snackId)
        {
            return _context.Snacks
                .FirstOrDefault(s => s.Id == snackId);
        }

        public IEnumerable<Snack> GetSnacks(string searchTerm = null, string categoryName = null)
        {
            IQueryable<Snack> query = _context.Snacks;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = $"%{searchTerm}%";
                query = query.Where(s => EF.Functions.Like(s.Name, searchTerm) || 
                    EF.Functions.Like(s.SummaryDescription, searchTerm) || 
                    EF.Functions.Like(s.FullDescription, searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                query = query.Where(s => s.Category.Name == categoryName);
            }

            return query
                .OrderByDescending(o => o.InStock)
                .ThenBy(o => o.Name);
        }
    }
}
