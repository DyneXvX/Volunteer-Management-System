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
        /*
        public IActionResult Upsert(int? id)
        {
            Opportunity opportunity = new Opportunity();

            if (id == null)         // Create (Insert)
            {
                return View(opportunity);
            }

            opportunity = _unitOfWork.Opportunity.Get(id.GetValueOrDefault());       // Edit (Update)

            if (opportunity == null)
            {
                return NotFound();
            }
            return View(opportunity);
        }
        */

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Opportunity.GetAll();
            return Json(new { data = allObj });
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