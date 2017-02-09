using Livit.Web.Models;
using ServiceStack.FluentValidation;
using System;

namespace Livit.Web.Validation
{
    public class RequestLeaveValidator : AbstractValidator<RequestLeave>
    {
        public RequestLeaveValidator()
        {
            RuleFor(r => r.AccessToken).NotEmpty();
            RuleFor(r => r.StartDate).LessThan(r => r.EndDate);
            RuleFor(r => r.StartDate).GreaterThan(DateTime.Today);
            RuleFor(r => r.Description).NotEmpty();
        }
    }
}
