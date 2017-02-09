using Google.Apis.Calendar.v3.Data;
using Livit.Common.Models;
using ServiceStack;
using System;

namespace Livit.Web.Models
{
    [Route("/api/requests", "POST")]
    public class RequestLeave : IReturn<RequestLeaveResponse>
    {
        public string AccessToken { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }

    public class RequestLeaveResponse
    {
        public LeaveRequest LeaveRequest { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
