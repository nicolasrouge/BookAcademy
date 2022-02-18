
using BookAcademy.DataAccess;
using BookAcademy.DataAccess.Repository.IRepository;
using BookAcademy.Models;
using BookAcademy.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BookAcademyWeb.Controllers
{
    [Area("Admin")]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();


            if (id is null || id == 0)
            {
                //create company
                return View(company);
            }
            else
            {
                //update prosuct
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);

            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//avoid crosssite request forgery
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                }
                else
                {
                    _unitOfWork.Company.Update(obj);

                }

                _unitOfWork.Save();
                TempData["success"] = "Company created succesfully";
                return RedirectToAction("Index");//we could reditrect to another contorller action
            }
            return View(obj);

        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }

}
