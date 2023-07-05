using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop2.Data;
using OnlineShop2.Models;
using OnlineShop2.Utility;
using System.Diagnostics;

namespace OnlineShop2.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var products = _db.Products.Include(x => x.ProductType).Include(y => y.SpecialTag).ToList();
            return View(products);
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

        // Get product detail action method
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductType).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Details")]
        public IActionResult ProductDetails(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductType).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null) 
            {
                products = new List<Product>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return View(product);
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");

            if (products != null)
            { 
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                { 
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction( nameof(Index) );
        }
    }
} 

