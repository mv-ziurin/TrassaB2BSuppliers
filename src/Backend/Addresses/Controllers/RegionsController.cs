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
    public class RegionsController : Controller
    {
        private readonly AddressesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public RegionsController(AddressesContext context)
        {
            _context = context;
        }

        public List<Region> Sort(DbSet<Region> regions, int from, int limit, string sortby)
        {
            if (typeof(Region).GetProperty(sortby.Substring(1)) == null)
                return new List<Region>();
            else if (sortby[0] == '-')
                return regions.OrderByDescending(r => r.GetType()
                                                       .GetProperty(sortby.Substring(1))
                                                       .GetValue(r, null))
                              .ToList().GetRange(from, limit);
            else
                return regions.OrderBy(r => r.GetType()
                                             .GetProperty(sortby.Substring(1))
                                             .GetValue(r, null))
                              .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<Region> Get()
        {
            if (_context.Regions.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Regions, from, limit, sortby);
            }
            else
                return new List<Region>();
        }

        [HttpGet("{id}", Name = "GetRegion")]
        public IActionResult Get(long id)
        {
            var item = _context.Regions.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Region item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Regions.Count() + 1;
            _context.Regions.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetRegion", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Region item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var region = _context.Regions.FirstOrDefault(t => t.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            region.CountryId = item.CountryId;
            region.Name = item.Name;

            _context.Regions.Update(region);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var region = _context.Regions.FirstOrDefault(t => t.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            _context.Regions.Remove(region);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
