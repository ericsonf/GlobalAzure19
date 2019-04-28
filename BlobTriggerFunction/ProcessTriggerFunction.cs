using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BlobTriggerFunction
{
    public static class ProcessTriggerFunction
    {
        [FunctionName("ProcessTriggerFunction")]
        public static async Task Run([BlobTrigger("process/{name}", Connection = "gabootcamp_STORAGE")]Stream myBlob, string name, [Blob("process/{name}", FileAccess.Read)] Stream inputBlob, [Blob("processed/{name}", FileAccess.Write)] Stream outputBlob, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            var sql = string.Empty;
            var orderIds = new List<string>();

            using (var sr = new StreamReader(myBlob))
            {
                while (!sr.EndOfStream)
                {
                    var line = await sr.ReadLineAsync();
                    var values = line.Split(';');
                    orderIds.Add(values[0]);
                    sql += $"INSERT INTO Purchases (OrderId, Name, Total, OrderDate) VALUES ('{values[0]}', '{values[1]}', '{values[2]}', '{values[3]}');";
                }
            }

            var str = Environment.GetEnvironmentVariable("sqlconnection");
            using (var conn = new SqlConnection(str))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var rows = await cmd.ExecuteNonQueryAsync();
                    log.LogInformation($"Data was imported.");
                }
            }

            await inputBlob.CopyToAsync(outputBlob);
            log.LogInformation($"File moved.");
        }
    }
}
