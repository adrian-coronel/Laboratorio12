using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Domain.Models;
using Domain.Models.payloads.request;

namespace Laboratorio11.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public ProductsController(InvoiceContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return _context.Products.Where(x => x.Enabled).ToList();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _context.Products.Where(x => x.Enabled && x.ProductId == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut]
        public ActionResult<Product> UpdatePrice(ProductRequestV2 request)
        {
            if (request.ProductId == 0)
                return BadRequest("Invalid ProductId");

            var product = _context.Products.Find(request.ProductId);
            if (product == null)
                return NotFound("Product not found");

            product.Price = request.Price;

            _context.Update(product);
            _context.SaveChanges();

            return product;
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Product> Save(ProductRequestV1 request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Enabled = true
            };
            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Enabled = false;

            _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteAll(List<int> productIds)
        {
            if (!productIds.Any()) return BadRequest();

            var products = _context.Products
                .Where(x => productIds.Contains(x.ProductId) && x.Enabled)
                .ToList();


            if (products.Any())
            {
                foreach (var product in products)
                    product.Enabled = false;

                _context.UpdateRange(products);
            }

            _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
