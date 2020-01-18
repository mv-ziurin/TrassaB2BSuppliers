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
    public class BrandConsumerProfilesRUController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public BrandConsumerProfilesRUController(SuppliersContext context)
        {
            _context = context;
        }

        public List<BrandConsumerProfileRU> Sort(DbSet<BrandConsumerProfileRU> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<BrandConsumerProfileRU>();
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
        public IEnumerable<BrandConsumerProfileRU> Get()
        {
            if (_context.BrandConsumerProfilesRU.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.BrandConsumerProfilesRU, from, limit, sortby);
            }
            else
                return new List<BrandConsumerProfileRU>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.BrandConsumerProfilesRU.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BrandConsumerProfileRU item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.BrandConsumerProfilesRU.Count() + 1;
            _context.BrandConsumerProfilesRU.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] BrandConsumerProfileRU item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var profile = _context.BrandConsumerProfilesRU.FirstOrDefault(t => t.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            profile.BrandId = item.BrandId;
            profile.ConsumerProfileId = item.ConsumerProfileId;

            _context.BrandConsumerProfilesRU.Update(profile);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var profile = _context.BrandConsumerProfilesRU.FirstOrDefault(t => t.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.BrandConsumerProfilesRU.Remove(profile);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}


