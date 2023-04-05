using Newtonsoft.Json;
using System.Collections.Generic;

namespace VerytelPizzaShop.Models
{
    public class Order
    {
        [JsonProperty("id_order")]
        public int Id { get; set; }

        [JsonProperty("addres")]
        public string Address { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("toppings")]
        public List<string> Toppings { get; set; }

        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
