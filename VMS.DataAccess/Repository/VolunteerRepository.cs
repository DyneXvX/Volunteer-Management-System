using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VMS.Data;
using VMS.DataAccess.Repository.IRepository;
using VMS.Models;

namespace VMS.DataAccess.Repository
{
    public class VolunteerRepository : Repository<Volunteer>, IVolunteerRepository
    {
        private readonly ApplicationDbContext _db;

        public VolunteerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Volunteer volunteer)
        {
            var objFromDb = _db.Volunteers.FirstOrDefault(s => s.Id == volunteer.Id);
            //update based on single Id from volunteers. 
            if (objFromDb != null)
            {
                objFromDb.FirstName = volunteer.FirstName;
                _db.SaveChanges();
            }
        }
    }
}
