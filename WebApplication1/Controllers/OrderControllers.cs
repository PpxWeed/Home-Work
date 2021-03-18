using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using HomeWork.Data;
using HomeWork.DTO;
using DTO;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly Context _context;

        public  OrderController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderTable()
        {
            return await _context.Order.ToListAsync();
        }

        [HttpPost("loan")]
        public async Task<ActionResult<IEnumerable<LoanProductsDTO>>> LoanProducts(LoanProductsDTO request)
        {
            foreach(var item in request.products)
            {
                var orders = new  Order()
                {
                    Customer_id = request.Customer_id,
                    Products_id = item,
                    Reservation = DateTime.UtcNow
                };
                await _context. Order.AddAsync(orders);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetOrderTable",request);
        }
    }
}