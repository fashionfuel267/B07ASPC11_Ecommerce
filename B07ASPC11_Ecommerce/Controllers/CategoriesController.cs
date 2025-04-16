using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using B07ASPC11_Ecommerce.Data;
using static System.Net.Mime.MediaTypeNames;

namespace B07ASPC11_Ecommerce.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment _host;
        public CategoriesController(ApplicationDbContext context,IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.OrderByDescending(c=>c.Id).ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewBag.ParentId = new SelectList(_context.Categories.OrderBy(c=>c.Name).ToList(),"Id","Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,Name,Image")] Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.Image!=null)
                {

                    string ext = Path.GetExtension(category.Image.FileName).ToLower();
                    if(ext ==".jpg" || ext == ".jpeg"|| ext == ".png")
                    {

                        try
                        {
                            string savePath = Path.Combine(_host.WebRootPath, "Pictures");
                            if (!Directory.Exists(savePath))
                            {
                                Directory.CreateDirectory(savePath);
                            }
                            string filePath = Path.Combine(savePath, category.Name + ext);
                            using (FileStream fs = new FileStream(filePath, FileMode.Create))
                            {
                                category.Image.CopyTo(fs);
                            }
                            category.ImagePath = "~/Pictures/" + category.Name + ext;
                            _context.Add(category);
                            if (await _context.SaveChangesAsync() > 0)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                ModelState.AddModelError("", "Save failed");
                              //  return View(category);
                            }
                        }
                        catch(Exception ex)
                        {
                            ModelState.AddModelError("", ex.Message);
                           // return View(category);

                        }

                       
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please enter valid image");
                       // return View(category);
                    }
                
                }
               else
                {
                    ModelState.AddModelError("", "Please enter valid image");
                   // return View(category);
                }
            }
            ViewBag.ParentId = new SelectList(_context.Categories.OrderBy(c => c.Name).ToList(), "Id", "Name");
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.ParentId = new SelectList(_context.Categories.OrderBy(c => c.Name).ToList(), "Id", "Name",category.ParentId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentId,Name,ImagePath,Image")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(category.Image!=null)
                    {
                        
                        string ext = Path.GetExtension(category.Image.FileName).ToLower();
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                        {
                            string savePath = Path.Combine(_host.WebRootPath, "Pictures");
                            string p = category.ImagePath.Remove(0, 2);
                            string oldPath =Path.Combine(_host.WebRootPath,p );
                            System.IO.File.Delete(oldPath);
                            if (!Directory.Exists(savePath))
                            {
                                Directory.CreateDirectory(savePath);
                            }
                            string filePath = Path.Combine(savePath, category.Name + ext);
                            using (FileStream fs = new FileStream(filePath, FileMode.Create))
                            {
                                category.Image.CopyTo(fs);
                            }
                            category.ImagePath = "~/Pictures/" + category.Name + ext;
                        }
                    }
                    else
                    {
                        category.Image = category.Image;
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                          .SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage));
                ModelState.AddModelError("", message);
            }
            ViewBag.ParentId = new SelectList(_context.Categories.OrderBy(c => c.Name).ToList(), "Id", "Name");
            return View(category);
        }

        // GET: Categories/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.Categories
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        // POST: Categories/Delete/5
        //[ ActionName("Delete")]
        
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
