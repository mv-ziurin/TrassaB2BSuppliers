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
    public class BrandCompetitorsController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public BrandCompetitorsController(SuppliersContext context)
        {
            _context = context;
        }

        public List<BrandCompetitor> Sort(DbSet<BrandCompetitor> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<BrandCompetitor>();
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
        public IEnumerable<BrandCompetitor> Get()
        {
            if (_context.BrandCompetitors.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.BrandCompetitors, from, limit, sortby);
            }
            else
                return new List<BrandCompetitor>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.BrandCompetitors.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BrandCompetitor item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.BrandCompetitors.Count() + 1;
            _context.BrandCompetitors.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] BrandCompetitor item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var competitor = _context.BrandCompetitors.FirstOrDefault(t => t.Id == id);
            if (competitor == null)
            {
                return NotFound();
            }

            competitor.BrandId = item.BrandId;
            competitor.CompetitorBrandId = item.CompetitorBrandId;
            competitor.CategoryTreeId = item.CategoryTreeId;

            _context.BrandCompetitors.Update(competitor);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var competitor = _context.BrandCompetitors.FirstOrDefault(t => t.Id == id);
            if (competitor == null)
            {
                return NotFound();
            }

            _context.BrandCompetitors.Remove(competitor);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}


