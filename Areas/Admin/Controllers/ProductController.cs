using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop2.Data;
using OnlineShop2.Models;

namespace OnlineShop2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;

        private IWebHostEnvironment _webHostEnvironment;
        

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var products = _db.Products.Include(x=> x.ProductType).Include(y=> y.SpecialTag).ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductTypeName");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile )
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    //var wwwroot = _webHostEnvironment.WebRootPath;
                    //var imageFileName = Path.GetFileName(image.FileName);
                    //var path = Path.Combine(wwwroot + "/Images", imageFileName);
                    var path = Path.Combine(_webHostEnvironment.WebRootPath + "/Images", Path.GetFileName(imageFile.FileName));

                    await imageFile.CopyToAsync(new FileStream(path, FileMode.Create));
                    product.ImageUrl = "Images/" + imageFile.FileName;

                }
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
