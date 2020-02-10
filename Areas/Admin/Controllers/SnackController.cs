using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Snacks.Context;
using Snacks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class SnackController : Controller
    {
        private readonly SnackContext _context;

        public SnackController(SnackContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var snacks = await _context.Snacks
                .Include(s => s.Category)
                .OrderBy(o => o.Name)
                .ToListAsync();

            return View(snacks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CategoryItems"] = GetCategoryItems();
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SummaryDescription,FullDescription,Price,ImageUrl,ImageThumbnailUrl,IsFavoriteSnack,InStock,CategoryId")] Snack snack)
        {
            if (snack.CategoryId == 0)
            {
                ModelState.AddModelError("CategoryId", "Selecione uma categoria");
            }

            if (ModelState.IsValid)
            {
                var category = await _context.Categories
                    .FindAsync(snack.CategoryId);

                if (category == null)
                {
                    ModelState.AddModelError("CategoryId", "Selecione uma categoria válida");
                }
                else
                {
                    _context.Add(snack);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }

            ViewData["CategoryItems"] = GetCategoryItems();
            return View(snack);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var snack = await _context.Snacks.FindAsync(id);
            if (snack == null)
            {
                return View("NotFound");
            }

            ViewData["CategoryItems"] = GetCategoryItems();
            return View(snack);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SummaryDescription,FullDescription,Price,ImageUrl,ImageThumbnailUrl,IsFavoriteSnack,InStock,CategoryId")] Snack snack)
        {
            if (id != snack.Id)
            {
                return View("NotFound");
            }

            if (snack.CategoryId == 0)
            {
                ModelState.AddModelError("CategoryId", "Selecione uma categoria");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = await _context.Categories
                        .FindAsync(snack.CategoryId);

                    if (category == null)
                    {
                        ModelState.AddModelError("CategoryId", "Selecione uma categoria válida");
                    }
                    else
                    {
                        _context.Update(snack);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SnackExists(snack.Id))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }

            ViewData["CategoryItems"] = GetCategoryItems();
            return View(snack);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var snack = await _context.Snacks
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (snack == null)
            {
                return View("NotFound");
            }

            return View(snack);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var snack = await _context.Snacks.FindAsync(id);
            _context.Snacks.Remove(snack);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool SnackExists(int id)
        {
            return _context.Snacks.Any(e => e.Id == id);
        }

        private List<SelectListItem> GetCategoryItems()
        {
            var result = new List<SelectListItem>();

            var categories = _context.Categories;
            foreach (var category in categories)
            {
                result.Add(new SelectListItem()
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }

            result = result
                .OrderBy(o => o.Text)
                .ToList();

            result.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = " - "
            });

            return result;
        }
    }
}
