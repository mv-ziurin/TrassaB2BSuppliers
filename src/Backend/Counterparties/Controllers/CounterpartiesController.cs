using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Counterparties.Database;
using Counterparties.Database.Models;

namespace Counterparties.Controllers
{
    [Route("api/[controller]")]
    public class CounterpartiesController : Controller
    {
        private readonly CounterpartiesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public CounterpartiesController(CounterpartiesContext context)
        {
            _context = context;
        }

        public List<Counterparty> Sort(DbSet<Counterparty> counterparties, int from, int limit, string sortby)
        {
            if (typeof(Counterparty).GetProperty(sortby.Substring(1)) == null)
                return new List<Counterparty>();
            else if (sortby[0] == '-')
                return counterparties.OrderByDescending(a => a.GetType()
                                                              .GetProperty(sortby.Substring(1))
                                                              .GetValue(a, null))
                                     .ToList().GetRange(from, limit);
            else
                return counterparties.OrderBy(a => a.GetType()
                                                    .GetProperty(sortby.Substring(1))
                                                    .GetValue(a, null))
                                     .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<Counterparty> Get()
        {
            if (_context.Counterparties.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Counterparties, from, limit, sortby);
            }
            else
                return new List<Counterparty>();
        }

        [HttpGet("{id}", Name = "GetCounterparty")]
        public IActionResult Get(long id)
        {
            var item = _context.Counterparties.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Counterparty item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Counterparties.Count() + 1;
            _context.Counterparties.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetCounterparty", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Counterparty item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var counterparty = _context.Counterparties.FirstOrDefault(t => t.Id == id);
            if (counterparty == null)
            {
                return NotFound();
            }

            counterparty.Age = item.Age;
            counterparty.EmployeesNumber = item.EmployeesNumber;
            counterparty.FullName = item.FullName;
            counterparty.GroupNameId = item.GroupNameId;
            counterparty.OrganizationForm = item.OrganizationForm;
            counterparty.OrganizationId = item.OrganizationId;
            counterparty.Role = item.Role;
            counterparty.ShortName = item.ShortName;

            _context.Counterparties.Update(counterparty);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var counterparty = _context.Counterparties.FirstOrDefault(t => t.Id == id);
            if (counterparty == null)
            {
                return NotFound();
            }

            _context.Counterparties.Remove(counterparty);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}