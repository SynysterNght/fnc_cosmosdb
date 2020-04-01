


namespace fnc_cosmosdb.Models
{
    using Newtonsoft.Json;
    public class Product
    {
        [JsonProperty("id")]
        public string ProductId { get; set; }
        [JsonProperty("provider")]
        public string Provider { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
    }
}
