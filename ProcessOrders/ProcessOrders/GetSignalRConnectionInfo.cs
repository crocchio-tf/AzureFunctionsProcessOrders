using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace ProcessOrders
{
    public static class GetSignalRConnectionInfo
    {
        [FunctionName("GetSignalRConnectionInfo")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "orders")]SignalRConnectionInfo connectionInfo,
            ILogger log)
        {
            log.LogInformation("GetSignalRInfo function processed a request.");

            return connectionInfo != null
                ? (ActionResult)new OkObjectResult(connectionInfo)
                : new NotFoundObjectResult("Failed to load SignalR Info.");
        }
    }
}
