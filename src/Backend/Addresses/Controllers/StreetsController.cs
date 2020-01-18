using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Addresses.Database.Models;
using Addresses.Database;
using Microsoft.EntityFrameworkCore;

namespace Addresses.Controllers
{
    [Route("api/[controller]")]
    public class StreetsController : Controller
    {
        private readonly AddressesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public StreetsController(AddressesContext context)
        {
            _context = context;
        }

        public List<Street> Sort(DbSet<Street> streets, int from, int limit, string sortby)
        {
            if (typeof(Street).GetProperty(sortby.Substring(1)) == null)
                return new List<Street>();
            else if (sortby[0] == '-')
                return streets.OrderByDescending(s => s.GetType()
                                                       .GetProperty(sortby.Substring(1))
                                                       .GetValue(s, null))
                              .ToList().GetRange(from, limit);
            else
                return streets.OrderBy(s => s.GetType()
                                             .GetProperty(sortby.Substring(1))
                                             .GetValue(s, null))
                              .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<Street> Get()
        {
            if (_context.Streets.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Streets, from, limit, sortby);
            }
            else
                return new List<Street>();
        }

        [HttpGet("{id}", Name = "GetStreet")]
        public IActionResult Get(long id)
        {
            var item = _context.Streets.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Street item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Streets.Count() + 1;
            _context.Streets.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetStreet", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Street item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var street = _context.Streets.FirstOrDefault(t => t.Id == id);
            if (street == null)
            {
                return NotFound();
            }

            street.LocalityId = item.LocalityId;
            street.Name = item.Name;
            street.StreetTypeId = item.StreetTypeId;

            _context.Streets.Update(street);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var street = _context.Streets.FirstOrDefault(t => t.Id == id);
            if (street == null)
            {
                return NotFound();
            }

            _context.Streets.Remove(street);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
