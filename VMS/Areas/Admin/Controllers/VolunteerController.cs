using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VMS.DataAccess.Repository.IRepository;
using VMS.Models;

namespace VMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VolunteerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VolunteerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        /*  NEED TO WORK ON IT, DO NOT CHANGE   -Lital
        public IActionResult Upsert(int? id)
        {
            Volunteer volunteer = new Volunteer();

            if (id == null)   // Create (Insert)
            {
                return View(volunteer);
            }

            volunteer = _unitOfWork.Volunteer.Get(id.GetValueOrDefault());       // Edit (Update)

            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }
        */

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Volunteer.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Volunteer.Get(id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error deleting Category";
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Volunteer.Remove(objFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Category successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}