
using BookAcademy.DataAccess;
using BookAcademy.DataAccess.Repository.IRepository;
using BookAcademy.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookAcademyWeb.Controllers
{
    [Area("Admin")]

    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//avoid crosssite request forgery
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType created succesfully";
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
            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id );

            if (coverType is null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//avoid crosssite request forgery
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType edited succesfully";
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
            //var coverType = _db.Categories.Find(id);
            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(coverType => coverType.Id == id);
            //var coverType2 = _db.Categories.SingleOrDefault(coverType => coverType.Id == id);

            if (coverType is null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]//avoid crosssite request forgery
        public IActionResult DeletePOST(int? id)
        {

            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted succesfully";
            return RedirectToAction("Index");//we could reditrect to another contorller action
            
            return View(obj);

        }
    }
}
