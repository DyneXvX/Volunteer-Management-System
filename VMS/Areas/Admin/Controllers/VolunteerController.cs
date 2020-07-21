using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VMS.DataAccess.Repository.IRepository;
using VMS.Models;
using VMS.Utility;

namespace VMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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

        //  Merv & Camille
        // Upsert -----------------------------------------------------

        public IActionResult Upsert(int? id)
        {
            Volunteer volunteer = new Volunteer();
            if (id == null) 
            {
                //this is for create
                //if empty return to the view an empty category
                return View(volunteer);
            }

            /*this is for edit

            returns default value of the data type of a collection if a 
            collection is empty or doesn't find any element that satisfies
            the condition
            */

            volunteer = _unitOfWork.Volunteer.Get(id.GetValueOrDefault());
            return View();

            //taking care of null/if id is incorrect
            if (volunteer == null) 
            {
                return NotFound();
            }
            //else
            return View(volunteer);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Volunteer volunteer)
        {
            if (ModelState.IsValid) //checks all the validations in the model to see if its true. Extra security
            {

                if (volunteer.Id == 0)
                {
                    _unitOfWork.Volunteer.Add(volunteer);
                }
                else 
                {
                    _unitOfWork.Volunteer.Update(volunteer);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            // else return back to volunteer page
            return View(volunteer);
        }
        //----------------------------------------------------------------------

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
                TempData["Error"] = "Error deleting Volunteer";
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Volunteer.Remove(objFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Volunteer successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}