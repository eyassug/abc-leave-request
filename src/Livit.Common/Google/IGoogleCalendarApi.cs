using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using System.Threading.Tasks;

namespace Livit.Common.Google
{
    public interface IGoogleCalendarApi
    {
        BaseClientService.Initializer CreateFromToken(string accessToken, string applicationName);
        GoogleAuthorizationCodeFlow.Initializer CreateAuthorizationCodeFlowInitializer(ClientSecrets clientSecrets, string[] scopes);
        Task<Event> CreateEventAsync(CalendarService calendarService, Event @event, string calendarId);
        Task<Userinfoplus> GetUserInfoAsync(Oauth2Service authService);
        Task<TokenResponse> ExchangeCodeForTokenAsync(GoogleAuthorizationCodeFlow flow, string code, string redirectUri);
    }
}
