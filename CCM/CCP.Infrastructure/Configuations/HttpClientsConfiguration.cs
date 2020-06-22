using CCM.Constants;
using System;
using System.Net.Http;

namespace CCP.Infrastructure.Configuations
{
    public class HttpClientsConfiguration
    {
        public void ConfigAccessHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri(ConstantValues.AccessClientAddress);
        }
    }
}
