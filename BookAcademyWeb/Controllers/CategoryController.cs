
using BookAcademy.DataAccess;
using BookAcademy.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookAcademyWeb.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//avoid crosssite request forgery
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order Cannot exacly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index");//we could reditrect to another contorller action
            }
            return View(obj);

        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id is null || id ==0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            //var category2 = _db.Categories.FirstOrDefault(category => category.Id == id);
            //var category2 = _db.Categories.SingleOrDefault(category => category.Id == id);

            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//avoid crosssite request forgery
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order Cannot exacly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category edited succesfully";
                return RedirectToAction("Index");//we could reditrect to another contorller action
            }
            return View(obj);

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            //var category2 = _db.Categories.FirstOrDefault(category => category.Id == id);
            //var category2 = _db.Categories.SingleOrDefault(category => category.Id == id);

            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]//avoid crosssite request forgery
        public IActionResult DeletePOST(int? id)
        {

            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index");//we could reditrect to another contorller action
            
            return View(obj);

        }
    }
}
