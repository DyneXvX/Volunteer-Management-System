using System;
using System.Linq;
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

        public IActionResult Upsert(int? id)
        {
            var opportunity = new Opportunity();
            if (id == null) return View(opportunity);

            opportunity = _unitOfWork.Opportunity.Get(id.GetValueOrDefault());


            if (opportunity == null) return NotFound();
            return View(opportunity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Opportunity opportunity)
        {
            if (!ModelState.IsValid) return View(opportunity);
            if (opportunity.Id == 0)
                _unitOfWork.Opportunity.Add(opportunity);
            else
                _unitOfWork.Opportunity.Update(opportunity);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Opportunity.GetAll();
            return Json(new {data = allObj});
        }

        [HttpGet]
        public IActionResult GetSingle(int id)
        {
            var volObj = _unitOfWork.Opportunity.Get(id);
            return Json(new {data = volObj});
        }


        [HttpGet]
        public IActionResult GetFilters(string filter)
        {
            var allObj = _unitOfWork.Opportunity.GetAll();

            if (allObj == null)
            {
                TempData["Error"] = "Error retrieving Opportunities";
                return Json(new {success = false, message = "Error while retrieving"});
            }

            if (filter == null) return Json(new {data = allObj});

            if (filter.Equals(SD.Filter_Date))
            {
                var today = DateTime.Now;
                Func<Opportunity, bool> last60Days = o => o.DatePosted > today.AddDays(-60);
                allObj = allObj.Where(last60Days);
                return Json(new {data = allObj});
            }

            if (filter.Equals("")) return Json(new {data = allObj});
            Func<Opportunity, bool> center = o => o.CenterType == filter;
            allObj = allObj.Where(center);
            return Json(new {data = allObj});
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Opportunity.Get(id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error deleting Opportunity";
                return Json(new {success = false, message = "Error while deleting"});
            }

            _unitOfWork.Opportunity.Remove(objFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Opportunity successfully deleted";
            return Json(new {success = true, message = "Delete Successful"});
        }

        #endregion
    }
}