using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Livit.Common.Models;
using ServiceStack.OrmLite;

namespace Livit.Common.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository, IDisposable
    {
        private readonly IDbConnection _db;
        public LeaveRequestRepository(IDbConnection db)
        {
            _db = db;
        }

        public void Add(LeaveRequest leaveRequest)
        {
            _db.Insert(leaveRequest);
        }


        public IEnumerable<LeaveRequest> GetAll()
        {
            return _db.Select<LeaveRequest>().OrderByDescending(r => r.DateTimeRequested);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
