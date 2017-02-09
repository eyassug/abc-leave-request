using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livit.Web.Models
{
    [Route("/api/auth/signin-google")]
    public class SignInGoogle
    {
        public string Code { get; set; }
    }
}
