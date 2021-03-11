using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeWork.Data;
using HomeWork.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly Context _context;

        public CustomerController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var custom = from customs in _context.Customer
            join custom_description in _context.Customer_description on customs.id equals custom_description.customers_id
            select new CustomerDTO
            {
                customers_id = customs.id,
                customers_numbers = customs.customers_numbers,
                customers_id_Id = custom_description.customers_id,
                customers_name = custom_description.customers_name,
                customers_occasion = custom_description.customers_occasion
                
            };

            return await custom.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetCustomers_byId(int id)
        {   
            var prod = from prods in _context.Products
            join products_descriptions in _context.Products_description on prods.id equals products_descriptions.products_id
            join orders in _context.Order on prods.id equals orders.Products_id
            join customs in _context.Customer on orders.Customer_id equals customs.id
            select new ProductsDTO
            {
                Products_id = prods.id,
                Product_name = prods.product_name,
                plats = products_descriptions.plats,
                drinks = products_descriptions.drinks,
                deserts = products_descriptions.deserts,
                id = orders.Id,
                Reservation = orders.Reservation
                
            };

            var prod_by_id = prod.ToList().Find(x => x.Products_id == id);
            var custom = from customs in _context.Customer
             join orders in _context.Order on customs.id equals orders.Customer_id
            join customs_description in _context.Customer_description on customs.id equals customs_description.customers_id
            select new CustomerDTO
            {
                customers_numbers = customs.customers_numbers,
                customers_id_Id = customs_description.customers_id,
                customers_id = customs.id,
                customers_name = customs_description.customers_name,
                customers_occasion = customs_description.customers_occasion,

                Production = prod.Where(x => x.Products_id == orders.Customer_id).ToList()
                
            };

            var custom_by_id = custom.ToList().Find(x => x.customers_id == id);

            if (custom_by_id == null)
            {
                return NotFound();
            }
            return custom_by_id;
        }
        
    }
}