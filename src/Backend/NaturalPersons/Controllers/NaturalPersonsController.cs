using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NaturalPersons.Database;
using NaturalPersons.Database.Models;
using System;

namespace NaturalPersons.Controllers
{
    public class NaturalPersonsController : Controller
    {
        private readonly NaturalPersonsContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public NaturalPersonsController(NaturalPersonsContext context)
        {
            _context = context;
        }

        public List<NaturalPerson> Sort(DbSet<NaturalPerson> naturalPerson, int from, int limit, string sortby)
        {
            if (typeof(NaturalPerson).GetProperty(sortby.Substring(1)) == null)
                return new List<NaturalPerson>();
            else if (sortby[0] == '-')
                return naturalPerson.OrderByDescending(n => n.GetType()
                                                             .GetProperty(sortby.Substring(1))
                                                             .GetValue(n, null))
                                     .ToList().GetRange(from, limit);
            else
                return naturalPerson.OrderBy(n => n.GetType()
                                                   .GetProperty(sortby.Substring(1))
                                                   .GetValue(n, null))
                                    .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<NaturalPerson> Get()
        {
            if (_context.NaturalPersons.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.NaturalPersons, from, limit, sortby);
            }
            else
                return new List<NaturalPerson>();
        }

        [HttpGet("{id}", Name = "GetNaturalPerson")]
        public IActionResult Get(long id)
        {
            var item = _context.NaturalPersons.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NaturalPerson item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.NaturalPersons.Count() + 1;
            _context.NaturalPersons.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetNaturalPerson", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] NaturalPerson item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var person = _context.NaturalPersons.FirstOrDefault(t => t.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            person.Surname = item.Surname;
            person.Name = item.Name;
            person.Patronymic = item.Patronymic;
            person.Sex = item.Sex;
            person.DateOfBirth = item.DateOfBirth;
            person.IdentityDocuments = item.IdentityDocuments;

            _context.NaturalPersons.Update(person);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var person = _context.NaturalPersons.FirstOrDefault(t => t.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            _context.NaturalPersons.Remove(person);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
