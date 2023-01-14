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
            //ViewData["TypeId"] = _db.ProductTypes.ToList().Select(
            //    i => new SelectListItem
            //    {
            //        Text = i.ProductTypeName,
            //        Value = i.Id.ToString()
            //    });
            //ViewData["TagId"] = _db.SpecialTags.ToList().Select(
            //    i => new SelectListItem
            //    {
            //        Text = i.Name,
            //        Value = i.Id.ToString()
            //    });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? imageFile )
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
                else
                {
                    product.ImageUrl = "Image/no-image-png-2.png";
                }
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Edit(int? id)
        {
            ViewData["TypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductTypeName");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");

            var product = _db.Products.Include(x => x.ProductType).Include(y => y.SpecialTag).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile? imageFile)
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
                else
                {
                    product.ImageUrl = "Image/no-image-png-2.png";
                }
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

    }
}
