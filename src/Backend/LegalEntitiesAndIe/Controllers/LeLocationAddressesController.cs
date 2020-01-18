using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LegalEntitiesAndIe.Database;
using LegalEntitiesAndIe.Database.Models;

namespace LegalEntitiesAndIe.Controllers
{
    [Route("api/[controller]")]
    public class LeLocationAddressesController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public LeLocationAddressesController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<LeLocationAddress> Sort(DbSet<LeLocationAddress> leLocationAddress, int from, int limit, string sortby)
        {
            if (typeof(LeLocationAddress).GetProperty(sortby.Substring(1)) == null)
                return new List<LeLocationAddress>();
            else if (sortby[0] == '-')
                return leLocationAddress.OrderByDescending(i => i.GetType()
                                                                 .GetProperty(sortby.Substring(1))
                                                                .GetValue(i, null))
                                        .ToList().GetRange(from, limit);
            else
                return leLocationAddress.OrderBy(i => i.GetType()
                                                       .GetProperty(sortby.Substring(1))
                                                       .GetValue(i, null))
                                        .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<LeLocationAddress> Get()
        {
            if (_context.LeLocationAddresses.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.LeLocationAddresses, from, limit, sortby);
            }
            else
                return new List<LeLocationAddress>();
        }

        [HttpGet("{id}", Name = "GetLeLocationAddress")]
        public IActionResult Get(long id)
        {
            var item = _context.LeLocationAddresses.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] LeLocationAddress item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.LeLocationAddresses.Count() + 1;
            _context.LeLocationAddresses.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetLeLocationAddress", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] LeLocationAddress item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var leLocationAddress = _context.LeLocationAddresses.FirstOrDefault(t => t.Id == id);
            if (leLocationAddress == null)
            {
                return NotFound();
            }

            leLocationAddress.AddressesId = item.AddressesId;
            leLocationAddress.EntryDateInEgrul = item.EntryDateInEgrul;
            leLocationAddress.Grn = item.Grn;
            leLocationAddress.LegalEntityId = item.LegalEntityId;

            _context.LeLocationAddresses.Update(leLocationAddress);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var leLocationAddress = _context.LeLocationAddresses.FirstOrDefault(t => t.Id == id);
            if (leLocationAddress == null)
            {
                return NotFound();
            }

            _context.LeLocationAddresses.Remove(leLocationAddress);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}