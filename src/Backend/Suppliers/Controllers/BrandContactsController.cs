using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Database;
using Suppliers.Database.Models;
using Microsoft.EntityFrameworkCore;


namespace Suppliers.Controllers
{
    [Route("api/[controller]")]
    public class BrandContactsController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public BrandContactsController(SuppliersContext context)
        {
            _context = context;
        }

        public List<BrandContact> Sort(DbSet<BrandContact> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<BrandContact>();
            else if (sortby[0] == '-')
                return Brandes.OrderByDescending(a => a.GetType()
                                                         .GetProperty(sortby.Substring(1))
                                                         .GetValue(a, null))
                                .ToList().GetRange(from, limit);
            else
                return Brandes.OrderBy(a => a.GetType()
                                               .GetProperty(sortby.Substring(1))
                                               .GetValue(a, null))
                                .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<BrandContact> Get()
        {
            if (_context.BrandContacts.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.BrandContacts, from, limit, sortby);
            }
            else
                return new List<BrandContact>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.BrandContacts.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BrandContact item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.BrandContacts.Count() + 1;
            _context.BrandContacts.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] BrandContact item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var contact = _context.BrandContacts.FirstOrDefault(t => t.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            contact.BrandId = item.BrandId;
            contact.ContactId = item.ContactId;

            _context.BrandContacts.Update(contact);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var contact = _context.BrandContacts.FirstOrDefault(t => t.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.BrandContacts.Remove(contact);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}
