using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LegalWorkers.Database;
using LegalWorkers.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LegalWorkers.Controllers
{
    [Route("api/[controller]")]
    public class UQPDsSectionController : Controller
    {
        private readonly LegalWorkersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public UQPDsSectionController(LegalWorkersContext context)
        {
            _context = context;
        }


        public List<UQPDSection> Sort(DbSet<UQPDSection> UQPDSection, int from, int limit, string sortby)
        {
            if (typeof(UQPDSection).GetProperty(sortby.Substring(1)) == null)
                return new List<UQPDSection>();
            else if (sortby[0] == '-')
                return UQPDSection.OrderByDescending(c => c.GetType()
                                                           .GetProperty(sortby.Substring(1))
                                                           .GetValue(c, null))
                                  .ToList().GetRange(from, limit);
            else
                return UQPDSection.OrderBy(c => c.GetType()
                                                 .GetProperty(sortby.Substring(1))
                                                 .GetValue(c, null))
                                  .ToList().GetRange(from, limit);
        }


        [HttpGet]
        public IEnumerable<UQPDSection> Get()
        {
            if (_context.EmployeeFunctions.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.UQPDSections, from, limit, sortby);
            }
            else
                return new List<UQPDSection>();
        }

        [HttpGet("{id}", Name = "GetUQPDSection")]
        public IActionResult Get(long id)
        {
            var item = _context.UQPDSections.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult UQPDSection([FromBody] UQPDSection item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.UQPDSections.Count() + 1;
            _context.UQPDSections.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUQPDSection", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] UQPDSection item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var section = _context.UQPDSections.FirstOrDefault(t => t.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            section.Name = item.Name;
            section.Description = item.Description;

            _context.UQPDSections.Update(section);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var UQPDSection = _context.UQPDSections.FirstOrDefault(t => t.Id == id);
            if (UQPDSection == null)
            {
                return NotFound();
            }

            _context.UQPDSections.Remove(UQPDSection);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
