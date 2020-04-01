
namespace fnc_cosmosdb
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using fnc_cosmosdb.Models;

    public class ProductInsert
    {
        [FunctionName(nameof(ProductInsert))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Product>(requestBody);

            var product = new Product
            {
                ProductId = data.ProductId,
                Provider=data.Provider,
                Name=data.Name,
                Price=data.Price
                
            };

            string responseMessage = product.Name;

            return new OkObjectResult(responseMessage);
        }
    }
}
