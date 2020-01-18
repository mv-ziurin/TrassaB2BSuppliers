using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaturalPersons.Database;
using NaturalPersons.Database.Models;

namespace NaturalPersons.Controllers
{
    public class IdentityDocumentsController : Controller
    {
        private readonly NaturalPersonsContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public IdentityDocumentsController(NaturalPersonsContext context)
        {
            _context = context;
        }


        public List<IdentityDocument> Sort(DbSet<IdentityDocument> identityDocument, int from, int limit, string sortby)
        {
            if (typeof(IdentityDocument).GetProperty(sortby.Substring(1)) == null)
                return new List<IdentityDocument>();
            else if (sortby[0] == '-')
                return identityDocument.OrderByDescending(i => i.GetType()
                                                                .GetProperty(sortby.Substring(1))
                                                                .GetValue(i, null))
                                       .ToList().GetRange(from, limit);
            else
                return identityDocument.OrderBy(i => i.GetType()
                                                      .GetProperty(sortby.Substring(1))
                                                      .GetValue(i, null))
                                       .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<IdentityDocument> Get()
        {
            if (_context.NaturalPersons.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.IdentityDocuments, from, limit, sortby);
            }
            else
                return new List<IdentityDocument>();
        }

        [HttpGet("{id}", Name = "GetIdentityDocument")]
        public IActionResult Get(long id)
        {
            var item = _context.IdentityDocuments.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IdentityDocument item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.IdentityDocuments.Count() + 1;
            _context.IdentityDocuments.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetIdentityDocument", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] IdentityDocument item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var document = _context.IdentityDocuments.FirstOrDefault(t => t.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            document.Type = item.Type;
            document.NaturalPersonId = item.NaturalPersonId;
            document.Number = item.Number;
            document.Name = item.Name;
            document.Surname = item.Surname;
            document.Patronymic = item.Patronymic;
            document.Sex = item.Sex;
            document.DateOfBirth = item.DateOfBirth;
            document.DateOfIssue = item.DateOfIssue;
            document.Authority = item.Authority;
            document.DateOfExpiration = item.DateOfExpiration;
            document.RegistrationAddressId = item.RegistrationAddressId;

        _context.IdentityDocuments.Update(document);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var document = _context.IdentityDocuments.FirstOrDefault(t => t.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            _context.IdentityDocuments.Remove(document);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
