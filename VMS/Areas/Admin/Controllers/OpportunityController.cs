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
    public class OpportunityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OpportunityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        //  Merv & Camille
        // Upsert 

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Opportunity.GetAll();
            return Json(new { data = allObj });
        }

        [HttpGet]
        public IActionResult GetSingle(int id)
        {
            var volObj = _unitOfWork.Opportunity.Get(id);
            return Json(new { data = volObj });
        }


        // Apply filters to Opportunities table 
        [HttpGet]
        public IActionResult GetFilters(string filter)
        {
            var allObj = _unitOfWork.Opportunity.GetAll();

            if (allObj == null)
            {
                TempData["Error"] = "Error retrieving Opportunities";
                return Json(new { success = false, message = "Error while retrieving" });
            }

            if (filter == null)
            {
                return Json(new { data = allObj });    // if no filters, return all
            }

            if (filter.Equals(SD.Filter_Date))
            {
                DateTime today = DateTime.Now;
                Func<Opportunity, bool> last60Days = o => o.DatePosted > today.AddDays(-60);
                allObj = allObj.Where(last60Days);
                return Json(new { data = allObj });
            }
            else if (filter.Equals(""))
            {
                return Json(new { data = allObj });    // if no filters, return all 
            }
            else  // center type
            {
                Func<Opportunity, bool> center = o => o.CenterType == filter;
                allObj = allObj.Where(center);
                return Json(new { data = allObj });
            }

            //return Json(new { data = allObj });    // if no filters, return all 
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Opportunity.Get(id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error deleting Opportunity";
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Opportunity.Remove(objFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Opportunity successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}