using ServiceStack;

namespace Livit.Web.Models
{
    [Route("/api/admin/requests/{Id}/reject", "POST")]
    public class RejectLeaveRequest
    {
        public int Id { get; set; }
    }
}
