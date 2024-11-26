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
    public class CustomersController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public CustomersController(InvoiceContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return _context.Customers.Where(x => x.Enabled).ToList();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = _context.Customers.Where(x => x.CustomerId == id && x.Enabled).FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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
        public IActionResult UpdateDocumentNumber(CustomerRequestV2 request)
        {
            if (request.CustomerId == 0)
                return BadRequest("Invalid CustomerId");

            var customer = _context.Customers.Find(request.CustomerId);
            if (customer == null)
                return NotFound("Customer not found");

            customer.DocumentNumber = request.DocumentNumber;

            _context.Update(customer);
            _context.SaveChanges();

            return NoContent();
        }


        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Customer> Save(CustomerRequestV1 request)
        {
            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DocumentNumber = request.DocumentNumber
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> SaveInvoices(CustomerRequestV3 request)
        {

            if (request.CustomerId == 0 || request.Invoices.Count == 0)
                return BadRequest();

            var customer = _context.Customers.Find(request.CustomerId);

            customer.Invoices = request.Invoices
                .Select(inv => new Invoice
                {
                    InvoiceNumber = inv.InvoiceNumber,
                    Date = inv.Date,
                    Total = inv.Total,
                    Enabled = true,
                }).ToList();

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Enabled = false;
            _context.Update(customer);
            _context.SaveChanges();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
