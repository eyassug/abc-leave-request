using Livit.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livit.Common.Repository
{
    public interface ILeaveRequestRepository : IRepository<LeaveRequest>
    {
        LeaveRequest GetById(int id);
        void UpdateStatus(int id, LeaveRequestStatus status);
    }
}
