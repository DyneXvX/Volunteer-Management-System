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

        // Apply filters to Volunteer table 
        [HttpGet]
        public IActionResult GetFilters(string filter)
        {
            var allObj = _unitOfWork.Volunteer.GetAll();

            if (allObj == null)
            {
                TempData["Error"] = "Error retrieving Volunteers";
                return Json(new { success = false, message = "Error while retrieving" });
            }

            if(filter == null)
            {
                return Json(new { data = allObj });    // if no filters, return all
            }
            
            if (filter.Equals(SD.Filter_Inactive))
            {
                Func<Volunteer, bool> inactive = v => !v.IsActive;
                allObj = allObj.Where(inactive);
                return Json(new { data = allObj });
            }
            else if (filter.Equals(SD.Filter_Approved))
            {
                Func<Volunteer, bool> approved = v => v.ApprovalStatus == SD.Status_Approved;
                allObj = allObj.Where(approved);
                return Json(new { data = allObj });
            }
            else if (filter.Equals(SD.Filter_Disapproved))
            {
                Func<Volunteer, bool> disapproved = v => v.ApprovalStatus == SD.Status_Disapproved;
                allObj = allObj.Where(disapproved);
                return Json(new { data = allObj });
            }
            else if (filter.Equals(SD.Filter_Pending))
            {
                Func<Volunteer, bool> pending = v => v.ApprovalStatus == SD.Status_Pending;
                allObj = allObj.Where(pending);
                return Json(new { data = allObj });
            }
            else if (filter.Equals(SD.Filter_Approved_Pending))
            {
                Func<Volunteer, bool> approvedOrPending = v => v.ApprovalStatus == SD.Status_Pending || v.ApprovalStatus == SD.Status_Approved;
                allObj = allObj.Where(approvedOrPending);
                return Json(new { data = allObj });
            }

            return Json(new { data = allObj });    // if no filters, return all 
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