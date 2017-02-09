using Livit.Common.Models;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Livit.Web.Models
{
    [Route("/api/admin/requests", "GET")]
    [Route("/api/admin/requests/{Id}", "GET")]
    public class GetLeaveRequests : IReturn<IEnumerable<LeaveRequestSummary>>, IAdminServiceModel
    {
        public int? Id { get; set; }
    }

    public class LeaveRequestSummary
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeRequested { get; set; }
        public LeaveRequestStatus LeaveRequestStatus { get; set; }
    }
}
