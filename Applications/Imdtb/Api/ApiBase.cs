using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Metis.Applications.Imdtb.Configuration;
using Microsoft.Extensions.Options;
using TechTalk.SpecFlow;

namespace Metis.Applications.Imdtb.Api
{
    [Binding]
    public class ApiBase
    {
        protected readonly IOptions<EnvironmentConfig> _environmentConfig;

        public ApiBase(IOptions<EnvironmentConfig> environmentConfig)
        {
            _environmentConfig = environmentConfig;
        }

        public string GetApiResponseString(string apiUrl)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            return streamReader.ReadToEnd();
        }

        public async Task PostApiRequest(string apiUrl, string jsonRequest)
        {
            using var client = new HttpClient();
            var response = await client.PostAsync(
                apiUrl,
                new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
        }
    }
}