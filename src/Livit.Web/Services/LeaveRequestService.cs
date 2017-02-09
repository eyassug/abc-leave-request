using System;
using Google.Apis.Calendar.v3;
using Google.Apis.Oauth2.v2;
using Livit.Common.Google;
using Livit.Common.Models;
using Livit.Web.Models;
using ServiceStack;
using ServiceStack.FluentValidation;
using System.Net;
using System.Threading.Tasks;
using Livit.Common.Repository;

namespace Livit.Web.Services
{
    public class LeaveRequestService : Service
    {
        public IGoogleCalendarApi GoogleApi { get; set; }
        public IValidator<RequestLeave> Validator { get; set; }
        public ILeaveRequestRepository Repository { get; set; }

        public async Task<object> Post(RequestLeave request)
        {
            var result = Validator.Validate(request);

            if (result.IsValid)
            {
                // Initialize UserCredential from token
                var initializer = GoogleApi.CreateFromToken(request.AccessToken, "ABC");
                var calendarService = new CalendarService(initializer);
                var authService = new Oauth2Service(initializer);

                var userInfo = await GoogleApi.GetUserInfoAsync(authService);

                var leaveRequest = new LeaveRequest
                {
                    FirstName = userInfo.GivenName ?? userInfo.Name,
                    LastName = userInfo.FamilyName,
                    Email = userInfo.Email,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Description = request.Description,
                    DateTimeRequested = DateTime.Now,
                    LeaveRequestStatus = LeaveRequestStatus.Pending,
                    AccessToken = request.AccessToken
                };
                Repository.Add(leaveRequest);
                return new HttpResult(new RequestLeaveResponse { LeaveRequest = leaveRequest }, HttpStatusCode.Created);
            }

            throw result.ToException();            
        }
    }
}
