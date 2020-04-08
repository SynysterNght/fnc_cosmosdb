
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
    using fnc_cosmosdb.Helpers;

    public class ProductInsert
    {
        [FunctionName(nameof(ProductInsert))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = null)] HttpRequest req,
			[CosmosDB(
                        databaseName: Constants.COSMOS_DB_DATABASE_NAME,
                        collectionName:Constants.COSMOS_DB_CONTAINER_NAME,
                        ConnectionStringSetting = "StrCosmos"    
                        )]
            IAsyncCollector<object> products,
            ILogger log)
        {
            IActionResult returnValue = null;
            try
            {
                
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Product>(requestBody);
                var product = new Product
                {
                    ProductId = data.ProductId,
                    Provider = data.Provider,
                    Name = data.Name,
                    Price = data.Price

                };
                ///Para insertarlo al CosmosDB
                await products.AddAsync(product);
                ///////////////
                log.LogInformation($"Item Inserted {product.Name}");
                
                returnValue = new OkObjectResult(product);
            }
            catch (Exception ex) {
                log.LogError($"Could not insert product. Exception: {ex.Message}");
                returnValue = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return returnValue;
        }
    }
}
