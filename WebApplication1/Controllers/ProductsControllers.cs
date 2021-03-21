using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webapplication1.Data;
using Webapplication1.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Webapplication1.Models;


namespace HomeWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly Context _context;

        public ProductsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsDTO>>> GetProducts()
        {
            var prod = from prods in _context.Products
            join products_descriptions in _context.Products_description on prods.id equals products_descriptions.products_id
            select new ProductsDTO
            {
                Products_id = prods.id,
                Product_name = prods.product_name,
                plats = products_descriptions.plats,
                drinks = products_descriptions.drinks,
                deserts = products_descriptions.deserts
            };

            return await prod.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductsDTO> GetProducts_byId(int id)
        {
            var prod = from prods in _context.Products
            join products_descriptions in _context.Products_description on prods.id equals products_descriptions.products_id
            select new ProductsDTO
            {
                Products_id = prods.id,
                Product_name = prods.product_name,
                plats = products_descriptions.plats,
                drinks = products_descriptions.drinks,
                deserts = products_descriptions.deserts
            };

            var prod_by_id = prod.ToList().Find(x => x.Products_id == id);

            if (prod_by_id == null)
            {
                return NotFound();
            }
            return prod_by_id;
        }

        [HttpPost]
        public async Task<ActionResult<AddProducts>> Add_Products(AddProducts productDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prod = new Products()
            {
                product_name = productDTO.Product_name
            };
            await _context.Products.AddAsync(prod);
            await _context.SaveChangesAsync();

            var prod_description = new Products_description()
            {
                plats = productDTO.Plats,
                drinks = productDTO.Drinks,
                deserts = productDTO.Deserts
            };
            await _context.AddAsync(prod_description);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProd", new { id = prod.id}, productDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> Delete_Product(int id)
        {
            var prod = _context.Products.Find(id);
            var prod_description = _context.Products_description.SingleOrDefault(x => x.products_id == id);

            if(prod == null)
            {
                return NotFound();
            }
            else 
            {
                _context.Remove(prod);
                _context.Remove(prod_description);
                await _context.SaveChangesAsync();
                return prod;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update_Products(int id, ProductsDTO product)
        {
            if(id != product.Products_id || !ProductsExists(id))
            {
                return BadRequest();
            }
            else 
            {
                var prod = _context.Products.SingleOrDefault(x => x.id == id);
                var prod_description = _context.Products_description.SingleOrDefault(x => x.products_id == id);

                product.Product_name = prod.product_name;
                product.deserts = prod_description.deserts;
                product.drinks = prod_description.drinks;
                product.plats = prod_description.plats;
               
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(x => x.id == id);
        }
    }
}