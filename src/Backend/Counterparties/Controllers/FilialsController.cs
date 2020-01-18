using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Counterparties.Database;
using Counterparties.Database.Models;

namespace Counterparties.Controllers
{
    [Route("api/[controller]")]
    public class FilialsController : Controller
    {
        private readonly CounterpartiesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public FilialsController(CounterpartiesContext context)
        {
            _context = context;
        }

        public List<Filial> Sort(DbSet<Filial> filials, int from, int limit, string sortby)
        {
            if (typeof(Filial).GetProperty(sortby.Substring(1)) == null)
                return new List<Filial>();
            else if (sortby[0] == '-')
                return filials.OrderByDescending(a => a.GetType()
                                                       .GetProperty(sortby.Substring(1))
                                                       .GetValue(a, null))
                              .ToList().GetRange(from, limit);
            else
                return filials.OrderBy(a => a.GetType()
                                             .GetProperty(sortby.Substring(1))
                                             .GetValue(a, null))
                              .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<Filial> Get()
        {
            if (_context.Filials.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Filials, from, limit, sortby);
            }
            else
                return new List<Filial>();
        }

        [HttpGet("{id}", Name = "GetFilial")]
        public IActionResult Get(long id)
        {
            var item = _context.Filials.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Filial item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Filials.Count() + 1;
            _context.Filials.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetFilial", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Filial item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var filial = _context.Filials.FirstOrDefault(t => t.Id == id);
            if (filial == null)
            {
                return NotFound();
            }

            filial.AddressId = item.AddressId;
            filial.CountepartyId = item.CountepartyId;
            filial.Name = item.Name;

            _context.Filials.Update(filial);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var filial = _context.Filials.FirstOrDefault(t => t.Id == id);
            if (filial == null)
            {
                return NotFound();
            }

            _context.Filials.Remove(filial);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}