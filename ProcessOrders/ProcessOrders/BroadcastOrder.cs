using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;

namespace ProcessOrders
{
    public static class BroadcastOrder
    {
        [FunctionName("BroadcastOrder")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "MyOrders",
            collectionName: "Orders",
            ConnectionStringSetting = "CosmosDBConnection",
            CreateLeaseCollectionIfNotExists =true
            )]IReadOnlyList<Document> newOrUpdatedOrders,
            [SignalR(HubName = "orders")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            if (newOrUpdatedOrders != null && newOrUpdatedOrders.Count > 0)
            {
                log.LogInformation("CosmosDB trigger BroadcastOrder function processed a request");

                foreach (var order in newOrUpdatedOrders)
                {
                    await signalRMessages.AddAsync(new SignalRMessage
                    {
                        Target = "broadcastOrder",
                        Arguments = new[] { order }
                    });
                }
            }
        }
    }
}
