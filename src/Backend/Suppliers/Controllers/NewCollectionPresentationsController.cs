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
    public class NewCollectionPresentationsController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public NewCollectionPresentationsController(SuppliersContext context)
        {
            _context = context;
        }

        public List<NewCollectionPresentation> Sort(DbSet<NewCollectionPresentation> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<NewCollectionPresentation>();
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
        public IEnumerable<NewCollectionPresentation> Get()
        {
            if (_context.NewCollectionPresentations.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.NewCollectionPresentations, from, limit, sortby);
            }
            else
                return new List<NewCollectionPresentation>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.NewCollectionPresentations.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewCollectionPresentation item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.NewCollectionPresentations.Count() + 1;
            _context.NewCollectionPresentations.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] NewCollectionPresentation item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var presentation = _context.NewCollectionPresentations.FirstOrDefault(t => t.Id == id);
            if (presentation == null)
            {
                return NotFound();
            }

            presentation.SeasonId = item.SeasonId;
            presentation.Venue = item.Venue;
            presentation.DateOfPerformance = item.DateOfPerformance;

            _context.NewCollectionPresentations.Update(presentation);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var presentation = _context.NewCollectionPresentations.FirstOrDefault(t => t.Id == id);
            if (presentation == null)
            {
                return NotFound();
            }

            _context.NewCollectionPresentations.Remove(presentation);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}


