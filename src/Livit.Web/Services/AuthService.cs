using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using Livit.Common.Google;
using Livit.Common.Models;
using Livit.Common.Repository;
using Livit.Web.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Livit.Web.Services
{
    
    public class AuthService : Service
    {
        const string ClientId = "139437536074-rq32s54n3l56hmltmtbo0e2pb24ek5t4.apps.googleusercontent.com";
        const string ClientSecret = "pUhfdy2pjAexju6fFt058ldK";
        const string RedirectUri = "https://localhost:44333/api/auth/signin-google";
        readonly IGoogleCalendarApi _googleApi;
        readonly GoogleAuthorizationCodeFlow _authFlow;

        public ITokenRepository TokenRepository { get; set; }
        #region Constructor
        public AuthService(IGoogleCalendarApi googleApi)
        {
            _googleApi = googleApi;
            var flowInitializer = _googleApi.CreateAuthorizationCodeFlowInitializer(new Google.Apis.Auth.OAuth2.ClientSecrets
            {
                ClientId = ClientId,
                ClientSecret = ClientSecret
            }, new string[] { CalendarService.Scope.Calendar });
            // Create auth flow
            _authFlow = new GoogleAuthorizationCodeFlow(flowInitializer);
        }
        #endregion

        public object Get(GetAuthUrl request)
        {
            var oauthUrl = string.Format("{0}?{1}", _authFlow.AuthorizationServerUrl, BuildOAuthQueryString());
            return new HttpResult(oauthUrl, HttpStatusCode.Created)
            {
                Location = oauthUrl
            };
        }
        
        public async Task<object> Get(SignInGoogle request)
        {
            var token = await _googleApi.ExchangeCodeForTokenAsync(this._authFlow, request.Code, RedirectUri);

            var t = token.ConvertTo<Token>();
            TokenRepository.AddOrUpdate(t);
            return new HttpResult(t, MimeTypes.Json);
        }

        #region Helper Methods
        static string BuildOAuthQueryString()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("response_type", "code");
            parameters.Add("client_id", ClientId);
            parameters.Add("redirect_uri", RedirectUri);
            parameters.Add("scope", string.Format("openid email {0}", CalendarService.Scope.Calendar));
            parameters.Add("access_type", "offline");

            var paramsArray = parameters.Select(p => string.Format("{0}={1}", WebUtility.HtmlEncode(p.Key), WebUtility.HtmlEncode(p.Value)));

            return string.Join("&", paramsArray);
        }
        #endregion
    }
}
