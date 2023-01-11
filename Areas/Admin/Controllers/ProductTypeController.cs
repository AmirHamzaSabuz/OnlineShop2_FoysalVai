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
                TempData["save"] = "Data has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }


        // Edit Get
        public IActionResult Edit(int? id)
        {
            if (id == null)
            { 
                return NotFound();
            }
            //var productType = _db.ProductTypes.Find(id);
            var productType  = _db.ProductTypes.FirstOrDefault(x => x.Id == id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
            
        }

        //Edit Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Update(productType);
                await _db.SaveChangesAsync();
                TempData["save"] = "Data has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // Details Get
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var productType = _db.ProductTypes.Find(id);
            var productType = _db.ProductTypes.FirstOrDefault(x => x.Id == id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);

        }

        // Delete Get
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var productType = _db.ProductTypes.Find(id);
            var productType = _db.ProductTypes.FirstOrDefault(x => x.Id == id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);

        }

        //Delete Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(/*int id,*/ ProductType productType)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            //if (id != productType.Id)
            //{
            //    return NotFound();
            //}
            //var product_Type = _db.ProductTypes.FirstOrDefault(x => x.Id == id);
            //if (product_Type == null)
            //{
            //    return NotFound();
            //}
            //if (ModelState.IsValid)
            //{
            //_db.ProductTypes.Remove(product_Type);
            _db.ProductTypes.Remove(productType);
            TempData["save"] = "Data has been deleted successfully.";
            await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(productType);
        }

    }
}
