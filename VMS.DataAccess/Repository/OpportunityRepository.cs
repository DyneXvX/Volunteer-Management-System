using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VMS.Data;
using VMS.DataAccess.Repository.IRepository;
using VMS.Models;

namespace VMS.DataAccess.Repository
{
    public class OpportunityRepository : Repository<Opportunity>, IOpportunityRepository
    {
        private readonly ApplicationDbContext _db;

        public OpportunityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Opportunity opportunity)
        {
            var objFromDb = _db.Opportunities.FirstOrDefault(s => s.Id == opportunity.Id);
            if (objFromDb != null)
            {
                objFromDb.OpportunityName = opportunity.OpportunityName;
                objFromDb.DatePosted = opportunity.DatePosted;
                objFromDb.CenterType = opportunity.CenterType;
                objFromDb.IsOpen = opportunity.IsOpen;
                objFromDb.VolunteerId = opportunity.VolunteerId;

                _db.SaveChanges();
            }
        }
    }
}
