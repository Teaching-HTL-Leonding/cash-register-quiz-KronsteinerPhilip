using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CashRegister.WebApi.Data
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }
    }
}
