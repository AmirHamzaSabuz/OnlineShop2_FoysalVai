using Microsoft.AspNetCore.Mvc;
using OnlineShop2.Data;
using OnlineShop2.Models;

namespace OnlineShop2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private ApplicationDbContext _db;

        public SpecialTagController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var data = _db.SpecialTags.ToList();
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
        public async Task<IActionResult> Create(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Add(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }


        // Edit Get
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var productType = _db.ProductTypes.Find(id);
            var specialTag = _db.SpecialTags.FirstOrDefault(x => x.Id == id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);

        }

        //Edit Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Update(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }

        // Details Get
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var specialTag = _db.SpecialTags.Find(id);
            var specialTag = _db.SpecialTags.FirstOrDefault(x => x.Id == id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);

        }

        // Delete Get
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var specialTag = _db.SpecialTags.Find(id);
            var specialTag = _db.SpecialTags.FirstOrDefault(x => x.Id == id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);

        }

        //Delete Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete( SpecialTag specialTag)
        {
            
            _db.SpecialTags.Remove(specialTag);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }
    }
}
