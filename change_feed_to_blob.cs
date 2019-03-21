using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json.Linq;

namespace cosmos_changefeed_csharp
{
    public static class change_feed_to_blob
    {
        [FunctionName("change_feed_to_blob")]    
        [StorageAccount("storageData")]
        public static void Run([CosmosDBTrigger(
            databaseName: "SensorData",
            collectionName: "Telemetry",
            ConnectionStringSetting = "cosmosdb",
            CreateLeaseCollectionIfNotExists = true,
            LeaseCollectionName = "leasescsharp")]JArray input,
            [Blob("outputcsharp/{rand-guid}.json", FileAccess.Write)] Stream outBlob,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                using (StreamWriter sw = new StreamWriter(outBlob)) {
                    sw.Write(input);
                    sw.Flush();
                }
            }
        }
    }
}
