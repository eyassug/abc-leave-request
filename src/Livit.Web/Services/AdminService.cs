using Livit.Common.Google;
using Livit.Common.Repository;
using Livit.Web.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Livit.Web.Services
{
    public class AdminService : Service
    {
        public ILeaveRequestRepository LeaveRequestRepository { get; set; }
        public IGoogleCalendarApi GoogleApi { get; set; }

        public object Get(GetLeaveRequests request)
        {
            var allRequests = LeaveRequestRepository.GetAll();
            return new HttpResult(allRequests, HttpStatusCode.OK);
        }

        public async Task<object> Post(ApproveLeaveRequest request)
        {
            // Check status

            // if already approved, return Http.Ok("already approved")

            // else create event in user's calendar, save status to database
            return request;
        }

        public async Task<object> Post(RejectLeaveRequest request)
        {
            // Check status

            // if already approved, return Http.BadRequest("already approved")

            // else save status to database
            return request;
        }


    }
}
