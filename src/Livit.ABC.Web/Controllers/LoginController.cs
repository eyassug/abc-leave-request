using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Livit.ABC.Web.Controllers
{
    [Route("api/auth")]
    public class LoginController : Controller
    {
        // GET: api/auth
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/auth/signin-google
        [HttpGet]
        [Route("signin-google")]
        public IEnumerable<string> Get(string code)
        {
            return new string[] { "value3", "value4" };
        }




    }
}
