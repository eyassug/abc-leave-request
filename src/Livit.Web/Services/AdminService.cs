using Google.Apis.Calendar.v3;
using Livit.Common.Google;
using Livit.Common.Models;
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
        public ITokenRepository TokenRepository { get; set; }
        public IGoogleCalendarApi GoogleApi { get; set; }

        public object Get(GetLeaveRequests request)
        {
            // Request Details
            if(request.Id == null)
            {
                // AutoMapping example
                var allRequests = LeaveRequestRepository.GetAll().Select(r => r.ConvertTo<LeaveRequestSummary>());
                return new HttpResult(allRequests, HttpStatusCode.OK);
            }
            var leaveRequest = LeaveRequestRepository.GetById(request.Id.Value);
            return leaveRequest != null ? new HttpResult(leaveRequest.ConvertTo<LeaveRequestSummary>()) : new HttpResult(HttpStatusCode.NotFound);
            
        }

        public async Task<object> Post(ApproveLeaveRequest request)
        {
            var leaveRequest = LeaveRequestRepository.GetById(request.Id);

            if (leaveRequest == null)
                return new HttpResult(HttpStatusCode.NotFound);

            // Check status
            if (leaveRequest.LeaveRequestStatus == LeaveRequestStatus.Approved)
                return new HttpResult("Request already approved", HttpStatusCode.OK);

            var token = TokenRepository.GetById(leaveRequest.TokenId);
            // Initialize UserCredential from token
            var initializer = GoogleApi.CreateFromToken(leaveRequest.Token.AccessToken, "ABC");
            var calendarService = new CalendarService(initializer);
            
            // Create event in user's calendar
            await GoogleApi.CreateEventAsync(calendarService, new Google.Apis.Calendar.v3.Data.Event
            {
                Summary = leaveRequest.Description ?? "Time off",
                Start = new Google.Apis.Calendar.v3.Data.EventDateTime
                {
                    DateTime = leaveRequest.StartDate
                },
                End = new Google.Apis.Calendar.v3.Data.EventDateTime
                {
                    DateTime = leaveRequest.EndDate
                },
                Description = leaveRequest.Description
            }, "primary");

            LeaveRequestRepository.UpdateStatus(request.Id, LeaveRequestStatus.Approved);
            return new HttpResult(HttpStatusCode.OK);
        }

        public object Post(RejectLeaveRequest request)
        {
            var leaveRequest = LeaveRequestRepository.GetById(request.Id);

            if (leaveRequest == null)
                return new HttpResult(HttpStatusCode.NotFound);
            
            // Check status
            if (leaveRequest.LeaveRequestStatus == LeaveRequestStatus.Approved)
                return new HttpResult("Request already approved", HttpStatusCode.BadRequest);

            LeaveRequestRepository.UpdateStatus(request.Id, LeaveRequestStatus.Rejected);
            return new HttpResult(HttpStatusCode.OK);
        }


    }
}
