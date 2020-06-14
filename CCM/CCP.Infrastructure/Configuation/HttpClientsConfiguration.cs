using System;
using System.Net.Http;

namespace CCP.Infrastructure.Configuation
{
    public class HttpClientsConfiguration
    {
        public void ConfigAccessHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44300/");
        }
    }
}
