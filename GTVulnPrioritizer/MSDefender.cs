using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GTVulnPrioritizer
{
    public class MSDefender : VulnerabilitySource
    {
        public MSDefender(OpenAIClient openAIClient) : base(openAIClient)
        {
        }

        public List<Vulnerability> GetVulnerabilities()
        {
            throw new NotImplementedException();
        }

        public static Vulnerability GetMSDefenderVulnerability()
        {
            string tenantId = "00000000-0000-0000-0000-000000000000"; // Paste your own tenant ID here
            string appId = "11111111-1111-1111-1111-111111111111"; // Paste your own app ID here
            string appSecret = "22222222-2222-2222-2222-222222222222"; // Paste your own app secret here for a test, and then store it in a safe place! 
            const string authority = "https://login.microsoftonline.com";
            const string audience = "https://api.securitycenter.microsoft.com";

            IConfidentialClientApplication myApp = ConfidentialClientApplicationBuilder.Create(appId).WithClientSecret(appSecret).WithAuthority($"{authority}/{tenantId}").Build();

            List<string> scopes = new List<string>() { $"{audience}/.default" };

            AuthenticationResult authResult = myApp.AcquireTokenForClient(scopes).ExecuteAsync().GetAwaiter().GetResult();

            string token = authResult.AccessToken;
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.securitycenter.microsoft.com/api/alerts");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "");

            

            // Do something useful with the response
            throw new NotImplementedException();

        }
    }
}
