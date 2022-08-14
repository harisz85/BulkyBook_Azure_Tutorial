using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Data;
using BulkyBookWeb.Models;

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
            IEnumerable<Category>? categoryList = _db.Categories;
            return View(categoryList);
        }

        //get method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category newCategory)
        {
           if(newCategory.Name == newCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "DisplayOrder and Name cannot match");
            }

           if(ModelState.IsValid)
            {
                _db.Categories?.Add(newCategory);
                _db.SaveChanges();
                TempData["success"] = "Category Created";
                return RedirectToAction("Index");
            }

            return View(newCategory);
        }


        //get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            } 

            var categoryToEdit = _db.Categories?.FirstOrDefault(c => c.Id == id);

            if(categoryToEdit == null)
            {
                return NotFound();
            }

            return View(categoryToEdit);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "DisplayOrder and Name cannot match");
            }

            if (ModelState.IsValid)
            {
                _db.Categories?.Update(category);
                _db.SaveChanges();
              //  TempData["success"] = "Category Updated";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryToDelete = _db.Categories?.FirstOrDefault(c => c.Id == id);

            if (categoryToDelete == null)
            {
                return NotFound();
            }

            return View(categoryToDelete);

        }

        //post


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRow(int? id)
        {
            var categoryToDelete = _db.Categories?.Find(id);

            if( categoryToDelete == null)
            {
                return NotFound();
            }
          
            _db.Categories?.Remove(categoryToDelete);
            _db.SaveChanges();
          //  TempData["success"] = "Category Removed";
            return RedirectToAction("Index");
        }



    }
}
