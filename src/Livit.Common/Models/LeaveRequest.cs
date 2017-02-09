using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livit.Common.Models
{
    public class LeaveRequest
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeRequested { get; set; }
        public LeaveRequestStatus LeaveRequestStatus { get; set; }
        public string AccessToken { get; set; }
    }
    [EnumAsInt]
    public enum LeaveRequestStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
