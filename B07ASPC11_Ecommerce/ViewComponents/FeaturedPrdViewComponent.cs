using B07ASPC11_Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace B07ASPC11_Ecommerce.ViewComponents
{
    public class FeaturedPrdViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public FeaturedPrdViewComponent(ApplicationDbContext context)
        {
            _context = context;
          
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Your logic for preparing data

            var product = _context.Products.OrderBy(a => a.Name).Where(p=>p.IsFeatured).ToList();
          
            return View(product);
        }

    }
}
