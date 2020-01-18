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
    public class ComparisonsByPriceController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public ComparisonsByPriceController(SuppliersContext context)
        {
            _context = context;
        }

        public List<ComparisonByPrice> Sort(DbSet<ComparisonByPrice> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<ComparisonByPrice>();
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
        public IEnumerable<ComparisonByPrice> Get()
        {
            if (_context.ComparisonsByPrice.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.ComparisonsByPrice, from, limit, sortby);
            }
            else
                return new List<ComparisonByPrice>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.ComparisonsByPrice.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ComparisonByPrice item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.ComparisonsByPrice.Count() + 1;
            _context.ComparisonsByPrice.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ComparisonByPrice item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var comp = _context.ComparisonsByPrice.FirstOrDefault(t => t.Id == id);
            if (comp == null)
            {
                return NotFound();
            }

            comp.BrandId = item.BrandId;
            comp.CompetitorBrandId = item.CompetitorBrandId;
            comp.CategoryTreeId = item.CategoryTreeId;
            comp.AveragePrice = item.AveragePrice;
            comp.PriceCompetitivenessIndex = item.PriceCompetitivenessIndex;

            _context.ComparisonsByPrice.Update(comp);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var comp = _context.ComparisonsByPrice.FirstOrDefault(t => t.Id == id);
            if (comp == null)
            {
                return NotFound();
            }

            _context.ComparisonsByPrice.Remove(comp);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}


