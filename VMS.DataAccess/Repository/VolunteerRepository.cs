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
                objFromDb.LastName = volunteer.LastName;
                objFromDb.UserName = volunteer.UserName;
                objFromDb.Password = volunteer.Password;
                objFromDb.VolunteerPrefersCenter = volunteer.VolunteerPrefersCenter;
                objFromDb.Skills = volunteer.Skills;
                objFromDb.Availability = volunteer.Availability;
                objFromDb.Address = volunteer.Address;
                objFromDb.HomePhone = volunteer.HomePhone;
                objFromDb.WorkPhone = volunteer.WorkPhone;
                objFromDb.CellPhone = volunteer.CellPhone;
                objFromDb.Email = volunteer.Email;
                objFromDb.Education = volunteer.Education;
                objFromDb.License = volunteer.License;
                objFromDb.EmergencyContactName = volunteer.EmergencyContactName;
                objFromDb.EmergencyContactHomePhone = volunteer.EmergencyContactHomePhone;
                objFromDb.EmergencyContactCellPhone = volunteer.EmergencyContactCellPhone;
                objFromDb.EmergencyContactEmail = volunteer.EmergencyContactEmail;
                objFromDb.IsDriversLicenseOnFile = volunteer.IsDriversLicenseOnFile;
                objFromDb.IsSsCardOnFile = volunteer.IsSsCardOnFile;
                objFromDb.IsActive = volunteer.IsActive;
                objFromDb.ApprovalStatus = volunteer.ApprovalStatus;

                _db.SaveChanges();
            }
        }
    }
}
