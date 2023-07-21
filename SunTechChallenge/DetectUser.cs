using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Serialization;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SunTechChallenge
{
    public static class DetectUser
    {
        [FunctionName("DetectUser")]
        public static async Task RunAsync([CosmosDBTrigger(
            databaseName: "NoSQLDB",
            containerName: "MyContainer",
            Connection = "CosmosDbConnectionString", 
            LeaseContainerName = "leases", 
            CreateLeaseContainerIfNotExists = true)]IReadOnlyList<Models.User> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("Document Id " + input[0].id);

                try
                {
                    var eventKey = Environment.GetEnvironmentVariable("EventGridKey");
                    var eventEndpoint = Environment.GetEnvironmentVariable("EventGridEndpoint");

                    EventGridPublisherClient client = new EventGridPublisherClient(new Uri(eventEndpoint), new AzureKeyCredential(eventKey));

                    List<EventGridEvent> eventsList = new List<EventGridEvent>
                    {
                        new EventGridEvent(
                            "New User",
                            "Saved to Cosmos DB",
                            "1.0",
                            input)
                    };

                    await client.SendEventsAsync(eventsList);
                }
                catch (Exception ex)
                {
                    log.LogError("Error with DetectUser: {0}", ex.Message);
                }          
            }
        }
    }
}
