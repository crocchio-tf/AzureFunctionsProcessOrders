using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Azure.Documents;

namespace ProcessOrders
{
    public static class GetOrders
    {
        [FunctionName("GetOrders")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "MyOrders",
                collectionName: "Orders",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "SELECT TOP 20 * FROM c ORDER BY c._ts DESC"
            )]IEnumerable<Document> documents,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(documents);
        }
    }
}
