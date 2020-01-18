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
    public class ComparisonsByPopularityController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public ComparisonsByPopularityController(SuppliersContext context)
        {
            _context = context;
        }

        public List<ComparisonByPopularity> Sort(DbSet<ComparisonByPopularity> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<ComparisonByPopularity>();
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
        public IEnumerable<ComparisonByPopularity> Get()
        {
            if (_context.ComparisonsByPopularity.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.ComparisonsByPopularity, from, limit, sortby);
            }
            else
                return new List<ComparisonByPopularity>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.ComparisonsByPopularity.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ComparisonByPopularity item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.ComparisonsByPopularity.Count() + 1;
            _context.ComparisonsByPopularity.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ComparisonByPopularity item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var comp = _context.ComparisonsByPopularity.FirstOrDefault(t => t.Id == id);
            if (comp == null)
            {
                return NotFound();
            }

            comp.BrandId = item.BrandId;
            comp.CompetitorBrandId = item.CompetitorBrandId;
            comp.CategoryTreeId = item.CategoryTreeId;
            comp.ConsumerProfileId = item.ConsumerProfileId;
            comp.PopularityScore = item.PopularityScore;

            _context.ComparisonsByPopularity.Update(comp);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var comp = _context.ComparisonsByPopularity.FirstOrDefault(t => t.Id == id);
            if (comp == null)
            {
                return NotFound();
            }

            _context.ComparisonsByPopularity.Remove(comp);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}


