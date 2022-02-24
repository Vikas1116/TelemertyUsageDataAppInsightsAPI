using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ConsumeAppInsigtsMetricsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelemetryController : ControllerBase
    {
        private const string AppId = "066ac5dc-4ec1-4e72-873e-44369d69de6a";

        private const string AzureAppInsightsEndpoint = "https://api.applicationinsights.io/v1/apps/{0}/events/customEvents?$filter=customDimensions%2F{1}%20eq%20%27{2}%27";

        private const string API_KEY = "ndibzy7ubhv8s5fdmzro0gfxcq50tfrge2aygjzg";

        private readonly ILogger<TelemetryController> _logger;

        public TelemetryController(ILogger<TelemetryController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTelemetry")]
        public string GetTelemetryData(string filterType, string filterName)
        {
            return GetAppInisghtsData(filterType, filterName);
        }

        private string GetAppInisghtsData(string filterType, string filterName)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", API_KEY);
            var req = string.Format(AzureAppInsightsEndpoint, AppId, filterType, filterName);
            HttpResponseMessage response = client.GetAsync(req).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return "";
            }
        }
    }
}
