using System;
using System.Collections.Generic;
using System.Text;

namespace VMS.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IVolunteerRepository Volunteer { get; }
        IOpportunityRepository Opportunity { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ISP_Call SP_Call { get; }

        void Save();
    }
}
