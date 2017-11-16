using Microsoft.Azure.CosmosDB.Table;
using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTableDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RunDemo().Wait();
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.Read();
        }

        public static async Task RunDemo()
        {
            var connectionString = "INSERT CONNECTION STRING HERE";
            
            var tableName = "demo" + Guid.NewGuid().ToString().Substring(0, 5);

            // Create Table
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();

            try
            {
                // Create row
                ARow aRow = new ARow("Partition Key", "Row Key", 42);

                // Insert Row
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(aRow);
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                aRow = result.Result as ARow;

                // Update Row
                aRow.count = 21;
                insertOrMergeOperation = TableOperation.InsertOrMerge(aRow);
                result = await table.ExecuteAsync(insertOrMergeOperation);

                // Retrieve Row
                TableOperation retrieveOperation = TableOperation.Retrieve<ARow>("Partition Key", "Row Key");
                result = await table.ExecuteAsync(retrieveOperation);
                aRow = result.Result as ARow;
                Console.WriteLine("And count is {0}", aRow.count);

                // Delete Row
                TableOperation deleteOperation = TableOperation.Delete(aRow);
                await table.ExecuteAsync(deleteOperation);
            }
            finally
            {
                await table.DeleteIfExistsAsync();
            }
        }
    }


}
