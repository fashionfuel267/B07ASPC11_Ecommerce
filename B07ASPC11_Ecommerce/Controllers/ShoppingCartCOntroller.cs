using B07ASPC11_Ecommerce.Data;
using B07ASPC11_Ecommerce.Data.Migrations;
using B07ASPC11_Ecommerce.Services;
using B07ASPC11_Ecommerce.SesionHelper;
using B07ASPC11_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace B07ASPC11_Ecommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _shoppingcart;
        public ShoppingCartController(ApplicationDbContext context, ShoppingCart shoppingcart)
        {
            _context = context;
            _shoppingcart = shoppingcart;
             
        }
        public IActionResult Index()
        {
            List<Cart> CartItems = new List<Cart>();
            if (HttpContext.Session.GetSessionObj<Cart>("cart") != null)
            {
                CartItems = HttpContext.Session.GetSessionObj<Cart>("cart");
            }
            ViewBag.TotalPrice = _shoppingcart.TotalPrice();
            return View(CartItems);
          ;
        }
        public IActionResult AddToCart(int id,int?  qty)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            _shoppingcart.AddToCart(product, qty ?? 1);
            HttpContext.Session.SetObjInsession("cart", _shoppingcart.CartItems);
            return RedirectToAction("Index", "Home");

           
        }
    }
}
