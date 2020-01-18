using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Counterparties.Database;
using Counterparties.Database.Models;

namespace CounterpartyGroupOwners.Controllers
{
    [Route("api/[controller]")]
    public class CounterpartyGroupOwnersController : Controller
    {
        private readonly CounterpartiesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public CounterpartyGroupOwnersController(CounterpartiesContext context)
        {
            _context = context;
        }

        public List<CounterpartyGroupOwner> Sort(DbSet<CounterpartyGroupOwner> counterpartyGroupOwners, int from, int limit, string sortby)
        {
            if (typeof(CounterpartyGroupOwner).GetProperty(sortby.Substring(1)) == null)
                return new List<CounterpartyGroupOwner>();
            else if (sortby[0] == '-')
                return counterpartyGroupOwners.OrderByDescending(a => a.GetType()
                                                                       .GetProperty(sortby.Substring(1))
                                                                       .GetValue(a, null))
                                              .ToList().GetRange(from, limit);
            else
                return counterpartyGroupOwners.OrderBy(a => a.GetType()
                                                             .GetProperty(sortby.Substring(1))
                                                             .GetValue(a, null))
                                              .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<CounterpartyGroupOwner> Get()
        {
            if (_context.CounterpartyGroupOwners.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.CounterpartyGroupOwners, from, limit, sortby);
            }
            else
                return new List<CounterpartyGroupOwner>();
        }

        [HttpGet("{id}", Name = "GetCounterpartyGroupOwner")]
        public IActionResult Get(long id)
        {
            var item = _context.CounterpartyGroupOwners.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CounterpartyGroupOwner item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.CounterpartyGroupOwners.Count() + 1;
            _context.CounterpartyGroupOwners.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetCounterpartyGroupOwner", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] CounterpartyGroupOwner item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var counterpartyGroupOwner = _context.CounterpartyGroupOwners.FirstOrDefault(t => t.Id == id);
            if (counterpartyGroupOwner == null)
            {
                return NotFound();
            }

            counterpartyGroupOwner.CounterpartyId = item.CounterpartyId;
            counterpartyGroupOwner.GroupOwnerId = item.GroupOwnerId;

            _context.CounterpartyGroupOwners.Update(counterpartyGroupOwner);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var counterpartyGroupOwner = _context.CounterpartyGroupOwners.FirstOrDefault(t => t.Id == id);
            if (counterpartyGroupOwner == null)
            {
                return NotFound();
            }

            _context.CounterpartyGroupOwners.Remove(counterpartyGroupOwner);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}