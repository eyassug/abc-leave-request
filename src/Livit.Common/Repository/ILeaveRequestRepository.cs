using Livit.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livit.Common.Repository
{
    public interface ILeaveRequestRepository : IRepository<LeaveRequest>
    {
        void UpdateStatus(int id, LeaveRequestStatus status);
    }
}
