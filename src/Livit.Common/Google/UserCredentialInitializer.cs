using Google.Apis.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Net.Http.Headers;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;

namespace Livit.Common.Google
{
    public class UserCredentialInitializer : IHttpExecuteInterceptor, IConfigurableHttpClientInitializer
    {
        private string _accessToken;

        public UserCredentialInitializer(string accessToken)
        {
            _accessToken = accessToken;
        }

        public void Initialize(ConfigurableHttpClient httpClient)
        {
            httpClient.MessageHandler.AddExecuteInterceptor(this);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task InterceptAsync(HttpRequestMessage request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        }
        
        public static BaseClientService.Initializer CreateServiceInitializer(string accessToken, string applicationName)
        {
            return new BaseClientService.Initializer
            {
                HttpClientInitializer = new UserCredentialInitializer(accessToken),
                ApplicationName = applicationName
            };
        }
    }
}
