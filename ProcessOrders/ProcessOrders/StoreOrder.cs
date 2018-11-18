using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;

namespace ProcessOrders
{
    public static class StoreOrder
    {
        [FunctionName("StoreOrder")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req,
            [CosmosDB(
                databaseName: "MyOrders",
                collectionName: "Orders",
                CreateIfNotExists = true,
                ConnectionStringSetting = "CosmosDBConnection")]
            IAsyncCollector<dynamic> outputDocuments,
            ILogger log)
        {
            log.LogInformation("HTTP trigger StoreOrder function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject(requestBody);

            data.confirmationNumber = Guid.NewGuid();

            await outputDocuments.AddAsync(data);

            return new OkObjectResult("Order stored");
        }
    }
}
