using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using B07ASPC11_Ecommerce.Models;
using B07ASPC11_Ecommerce.Data;

namespace B07ASPC11_Ecommerce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        this._context = context;
    }

    public IActionResult Index()
    {
        ViewBag.parentCategory = _context.Categories.Where(c => c.ParentId == 0).OrderBy(a => a.Name).ToList();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
