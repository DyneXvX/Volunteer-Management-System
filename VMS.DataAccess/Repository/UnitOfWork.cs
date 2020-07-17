using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using VMS.Data;
using VMS.DataAccess.Repository.IRepository;

namespace VMS.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Volunteer = new VolunteerRepository(_db);
            Opportunity = new OpportunityRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public IVolunteerRepository Volunteer { get; private set; }
        public IOpportunityRepository Opportunity { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        //not saving inside the IRepository Remember to build save method. JT
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
