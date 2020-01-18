using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Addresses.Database;
using Addresses.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Addresses.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly AddressesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public CountriesController(AddressesContext context)
        {
            _context = context;
        }

        public List<Country> Sort(DbSet<Country> countries, int from, int limit, string sortby)
        {
            if (typeof(Country).GetProperty(sortby.Substring(1)) == null)
                return new List<Country>();
            else if (sortby[0] == '-')
                return countries.OrderByDescending(c => c.GetType()
                                                         .GetProperty(sortby.Substring(1))
                                                         .GetValue(c, null))
                                .ToList().GetRange(from, limit);
            else 
                return countries.OrderBy(c => c.GetType()
                                                .GetProperty(sortby.Substring(1))
                                                .GetValue(c, null))
                                .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<Country> Get()
        {
            if (_context.Countries.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Countries, from, limit, sortby);
            }
            else
                return new List<Country>();
        }

        [HttpGet("{id}", Name = "GetCountry")]
        public IActionResult Get(long id)
        {
            var item = _context.Countries.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Country item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Countries.Count() + 1;
            _context.Countries.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetCountry", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Country item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var country = _context.Countries.FirstOrDefault(t => t.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            country.Alpha2Code = item.Alpha2Code;
            country.Alpha3Code = item.Alpha3Code;
            country.Name = item.Name;

            _context.Countries.Update(country);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var country = _context.Countries.FirstOrDefault(t => t.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
