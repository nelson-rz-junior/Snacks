using Microsoft.AspNetCore.Mvc;
using Snacks.Repositories.Interfaces;
using System.Linq;

namespace Snacks.Components
{
    public class MenuCategory : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public MenuCategory(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke(string dropDownText)
        {
            ViewData["DropDownText"] = dropDownText;

            var categories = _categoryRepository.Categories
                .OrderBy(o => o.Name);

            return View(categories);
        }
    }
}
