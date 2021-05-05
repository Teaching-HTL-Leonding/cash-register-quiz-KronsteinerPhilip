using CashRegister.WebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CashRegister.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly CashRegisterDataContext DataContext;

        public ReceiptsController(CashRegisterDataContext dataContext)
        {
            DataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ReceiptLineDto> receiptLineDto)
        {
            if (receiptLineDto == null || receiptLineDto.Count.Equals(1))
                return BadRequest();


            // Read product data from DB for incoming product IDs
            var products = new Dictionary<int, Product>();
            foreach (var item in receiptLineDto)
            {
                products[item.ProductID] = await DataContext.Products.FirstOrDefaultAsync(p => p.ID == item.ProductID);
                if (products[item.ProductID] == null)
                    return BadRequest();
            }

            // Here you have to add code that reads all products referenced by product IDs
            // in receiptDto.Lines and store them in the `products` dictionary.

            // Build receipt from DTO
            var newReceipt = new Receipt
            {
                ReceiptTimestamp = DateTime.UtcNow,
                ReceiptLines = receiptLineDto.Select(rl => new ReceiptLine
                {
                    ID = 0,
                    Product = products[rl.ProductID],
                    Amount = rl.Amount,
                    TotalPrice = rl.Amount * products[rl.ProductID].UnitPrice
                }).ToList()
            };
            newReceipt.TotalPrice = newReceipt.ReceiptLines.Sum(rl => rl.TotalPrice);

            await DataContext.Receipts.AddAsync(newReceipt);
            await DataContext.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.Created, newReceipt);
        }
    
    }

    public class ReceiptLineDto
    {
        public int ProductID { get; set; }
        public int Amount { get; set; }
    }
}

