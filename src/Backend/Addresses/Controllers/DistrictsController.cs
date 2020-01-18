using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Addresses.Database;
using Addresses.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Addresses.Controllers
{
    [Route("api/[controller]")]
    public class DistrictsController : Controller
    {
        private readonly AddressesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public DistrictsController(AddressesContext context)
        {
            _context = context;
        }

        public List<District> Sort(DbSet<District> districts, int from, int limit, string sortby)
        {
            if (typeof(District).GetProperty(sortby.Substring(1)) == null)
                return new List<District>();
            else if (sortby[0] == '-')
                return districts.OrderByDescending(d => d.GetType()
                                                         .GetProperty(sortby.Substring(1))
                                                         .GetValue(d, null))
                                .ToList().GetRange(from, limit);
            else
                return districts.OrderBy(d => d.GetType()
                                               .GetProperty(sortby.Substring(1))
                                               .GetValue(d, null))
                                .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<District> Get()
        {
            if (_context.Districts.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Districts, from, limit, sortby);
            }
            else
                return new List<District>();
        }

        [HttpGet("{id}", Name = "GetDistrict")]
        public IActionResult Get(long id)
        {
            var item = _context.Districts.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] District item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Districts.Count() + 1;
            _context.Districts.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetDistrict", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] District item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var district = _context.Districts.FirstOrDefault(t => t.Id == id);
            if (district == null)
            {
                return NotFound();
            }

            district.LocalityId = item.LocalityId;
            district.Name = item.Name;

            _context.Districts.Update(district);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var district = _context.Districts.FirstOrDefault(t => t.Id == id);
            if (district == null)
            {
                return NotFound();
            }

            _context.Districts.Remove(district);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
