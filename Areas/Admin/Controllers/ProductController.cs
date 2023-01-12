using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop2.Data;
using OnlineShop2.Models;

namespace OnlineShop2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var products = _db.Products.Include(x=> x.ProductType).Include(y=> y.SpecialTag).ToList();
            return View(products);
        }

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product )
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
