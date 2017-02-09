using Livit.Web.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livit.Web.Services
{
    
    public class AuthService : Service
    {
        
        public object Get(GetAuthUrl request)
        {
            return request;
        }
        
        public object Get(SignInGoogle request)
        {
            return request;
        }
    }
}
