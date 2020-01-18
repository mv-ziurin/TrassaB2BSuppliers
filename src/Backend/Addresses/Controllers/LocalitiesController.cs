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
    public class LocalitiesController : Controller

    {
        private readonly AddressesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public LocalitiesController(AddressesContext context)
        {
            _context = context;
        }

        public List<Locality> Sort(DbSet<Locality> localities, int from, int limit, string sortby)
        {
            if (typeof(Locality).GetProperty(sortby.Substring(1)) == null)
                return new List<Locality>();
            else if (sortby[0] == '-')
                return localities.OrderByDescending(l => l.GetType()
                                                          .GetProperty(sortby.Substring(1))
                                                          .GetValue(l, null))
                                 .ToList().GetRange(from, limit);
            else
                return localities.OrderBy(l => l.GetType()
                                                .GetProperty(sortby.Substring(1))
                                                .GetValue(l, null))
                                .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<Locality> Get()
        {
            if (_context.Localities.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Localities, from, limit, sortby);
            }
            else
                return new List<Locality>();
        }

        [HttpGet("{id}", Name = "GetLocality")]
        public IActionResult Get(long id)
        {
            var item = _context.Localities.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Locality item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Localities.Count() + 1;
            _context.Localities.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetLocality", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Locality item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var locality = _context.Localities.FirstOrDefault(t => t.Id == id);
            if (locality == null)
            {
                return NotFound();
            }

            locality.CountryId = item.CountryId;
            locality.Name = item.Name;
            locality.LocalityTypeId = item.LocalityTypeId;
            locality.RegionId = item.RegionId;

            _context.Localities.Update(locality);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var locality = _context.Localities.FirstOrDefault(t => t.Id == id);
            if (locality == null)
            {
                return NotFound();
            }

            _context.Localities.Remove(locality);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
