using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LegalEntitiesAndIe.Database;
using LegalEntitiesAndIe.Database.Models;

namespace LegalEntitiesAndIe.Controllers
{
    [Route("api/[controller]")]
    public class LeNamesController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public LeNamesController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<LeName> Sort(DbSet<LeName> leName, int from, int limit, string sortby)
        {
            if (typeof(LeName).GetProperty(sortby.Substring(1)) == null)
                return new List<LeName>();
            else if (sortby[0] == '-')
                return leName.OrderByDescending(i => i.GetType()
                                                      .GetProperty(sortby.Substring(1))
                                                      .GetValue(i, null))
                             .ToList().GetRange(from, limit);
            else
                return leName.OrderBy(i => i.GetType()
                                            .GetProperty(sortby.Substring(1))
                                            .GetValue(i, null))
                             .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<LeName> Get()
        {
            if (_context.LeNames.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.LeNames, from, limit, sortby);
            }
            else
                return new List<LeName>();
        }

        [HttpGet("{id}", Name = "GetLeName")]
        public IActionResult Get(long id)
        {
            var item = _context.LeNames.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] LeName item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.LeNames.Count() + 1;
            _context.LeNames.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetLeName", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] LeName item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var leName = _context.LeNames.FirstOrDefault(t => t.Id == id);
            if (leName == null)
            {
                return NotFound();
            }

            leName.EntryDateInEgrul = item.EntryDateInEgrul;
            leName.FullName = item.FullName;
            leName.Grn = item.Grn;
            leName.LegalEntityId = item.LegalEntityId;
            leName.ShortName = item.ShortName;

            _context.LeNames.Update(leName);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var leName = _context.LeNames.FirstOrDefault(t => t.Id == id);
            if (leName == null)
            {
                return NotFound();
            }

            _context.LeNames.Remove(leName);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}