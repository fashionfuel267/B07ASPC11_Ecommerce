using B07ASPC11_Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace B07ASPC11_Ecommerce.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public CategoryListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? catid)
        {
            // Your logic for preparing data
            
            var categories = _context.Categories.OrderBy(a => a.Name).ToList();
            if(catid.HasValue)
            {
                categories = categories.Where(c => c.ParentId == catid).ToList();
            }
            else
            {
                categories = categories.Where(c => c.ParentId == 0).ToList();
            }
            return View(categories);
        }

    }
}
