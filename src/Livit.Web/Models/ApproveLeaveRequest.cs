using ServiceStack;

namespace Livit.Web.Models
{
    [Route("/api/admin/requests/{Id}/approve", "POST")]
    public class ApproveLeaveRequest
    {
        public int Id { get; set; }
    }
}
