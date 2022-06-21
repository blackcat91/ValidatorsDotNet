using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.OrderBy(p => p.DisplayOrder);
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            
            var dupName = _db.Categories.FirstOrDefault(c => c.Name == obj.Name);
            var dupOrder = _db.Categories.FirstOrDefault(c=> c.DisplayOrder == obj.DisplayOrder);

            if (dupName != null)
            {
                ModelState.AddModelError("name", "Name already exists");
            }

            if (dupOrder != null)
            {
                ModelState.AddModelError("displayorder", "Order already exists");
            }
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DislayOrder can not match Name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

      


        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            // var catFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var catSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if(category == null)
            {
                return NotFound();
            }
            

            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {

            var dupName = _db.Categories.FirstOrDefault(c => c.Name == obj.Name);
            var dupOrder = _db.Categories.FirstOrDefault(c => c.DisplayOrder == obj.DisplayOrder);

            if (dupName != null)
            {
                ModelState.AddModelError("name", "Name already exists");
            }

            if (dupOrder != null)
            {
                ModelState.AddModelError("displayorder", "Order already exists");
            }
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DislayOrder can not match Name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            // var catFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var catSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }


            return View(category);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
          

           
             _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
            
            return View(obj);
        }

    }
}
