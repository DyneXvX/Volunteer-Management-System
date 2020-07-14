using System;
using System.Collections.Generic;
using System.Text;
using VMS.Models;

namespace VMS.DataAccess.Repository.IRepository
{
    public interface IVolunteerRepository : IRepository<Volunteer>
    {
        void Update(Volunteer volunteer);
    }
}
