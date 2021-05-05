using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CashRegister.WebApi.Data
{
    public class ReceiptLine
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
    }
}
