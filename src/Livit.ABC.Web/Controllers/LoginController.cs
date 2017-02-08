using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Livit.Common.Google;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using System.Linq;
using ServiceStack.Pcl;
using System.Net;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Livit.ABC.Web.Controllers
{
    [Route("api/auth")]
    public class LoginController : Controller
    {
        #region Fields
        const string ClientId = "139437536074-rq32s54n3l56hmltmtbo0e2pb24ek5t4.apps.googleusercontent.com";
        const string ClientSecret = "pUhfdy2pjAexju6fFt058ldK";
        const string RedirectUri = "https://localhost:44359/api/auth/signin-google";
        readonly IGoogleCalendarApi _googleApi;
        readonly GoogleAuthorizationCodeFlow _authFlow;

        #endregion

        #region Constructor
        public LoginController(IGoogleCalendarApi googleApi)
        {
            _googleApi = googleApi;
            // Create auth flow initializer
            var flowInitializer = _googleApi.CreateAuthorizationCodeFlowInitializer(new Google.Apis.Auth.OAuth2.ClientSecrets
            {
                ClientId = ClientId,
                ClientSecret = ClientSecret
            }, new string[] { CalendarService.Scope.Calendar });
            // Create auth flow
            _authFlow = new GoogleAuthorizationCodeFlow(flowInitializer);
        }

        #endregion

        // GET: api/auth
        [HttpGet]
        public IActionResult Get()
        {
            var oauthUrl = string.Format("{0}?{1}", _authFlow.AuthorizationServerUrl, BuildOAuthQueryString());
            return Redirect(oauthUrl);
        }

        // GET: api/auth/signin-google
        [HttpGet]
        [Route("signin-google")]
        public IEnumerable<string> Get(string code)
        {
            return new string[] { "value3", "value4" };
        }

        #region Helper Methods
        static string BuildOAuthQueryString()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("response_type", "code");
            parameters.Add("client_id", ClientId);
            parameters.Add("redirect_uri", RedirectUri);
            parameters.Add("scope", CalendarService.Scope.Calendar);
            parameters.Add("access_type", "offline");

            var paramsArray = parameters.Select(p => string.Format("{0}={1}", WebUtility.HtmlEncode(p.Key), WebUtility.HtmlEncode(p.Value)));

            return string.Join("&", paramsArray);
        }
        #endregion


    }
}
