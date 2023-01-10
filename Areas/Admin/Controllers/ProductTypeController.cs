using Microsoft.AspNetCore.Mvc;
using OnlineShop2.Data;
using OnlineShop2.Models;

namespace OnlineShop2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypeController : Controller
    {
        private ApplicationDbContext _db;

        public ProductTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var data = _db.ProductTypes.ToList();
            return View(data);
        }

        // Create Get
        public IActionResult Create() 
        {
            return View();
        }

        //Create Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductType productType)
        {
            if (ModelState.IsValid)
            { 
                _db.ProductTypes.Add(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

    }
}
