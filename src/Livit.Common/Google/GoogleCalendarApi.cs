using System;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using System.Threading;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;

namespace Livit.Common.Google
{
    public class GoogleCalendarApi : IGoogleCalendarApi
    {
        public async Task<Event> CreateEventAsync(CalendarService calendarService, Event @event, string calendarId)
        {
            var request = calendarService.Events.Insert(@event, calendarId);
            return await request.ExecuteAsync();
        }

        public BaseClientService.Initializer CreateFromToken(string accessToken, string applicationName)
        {
            return new BaseClientService.Initializer
            {
                HttpClientInitializer = new UserCredentialInitializer(accessToken),
                ApplicationName = applicationName
            };
        }

       
        public async Task<Userinfoplus> GetUserInfoAsync(Oauth2Service authService)
        {
            var request = authService.Userinfo.Get();
            return await request.ExecuteAsync();
        }

        public async Task<TokenResponse> ExchangeCodeForTokenAsync(GoogleAuthorizationCodeFlow flow, string code, string redirectUri)
        {
            var userId = "user-id";

            return await flow.ExchangeCodeForTokenAsync(userId, code, redirectUri, CancellationToken.None);
        }

        public GoogleAuthorizationCodeFlow.Initializer CreateAuthorizationCodeFlowInitializer(ClientSecrets clientSecrets, string[] scopes)
        {
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = clientSecrets,
                Scopes = scopes
            };

            return initializer;
        }
    }
}
