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


        public LeaveRequest GetById(int id)
        {
            return _db.Select<LeaveRequest>(r => r.Id == id).SingleOrDefault();
        }

        public void UpdateStatus(int id, LeaveRequestStatus status)
        {
            _db.UpdateOnly(() => new LeaveRequest { LeaveRequestStatus = status }, where: r => r.Id == id);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
