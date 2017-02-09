using ServiceStack;

namespace Livit.Web.Models
{
    [Route("/api/admin/requests/{Id}/reject", "POST")]
    public class RejectLeaveRequest : IAdminServiceModel
    {
        public int Id { get; set; }
    }
}
