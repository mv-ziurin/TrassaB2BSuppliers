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
    public class IeFullNamesController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public IeFullNamesController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<IeFullName> Sort(DbSet<IeFullName> ieFullNamees, int from, int limit, string sortby)
        {
            if (typeof(IeFullName).GetProperty(sortby.Substring(1)) == null)
                return new List<IeFullName>();
            else if (sortby[0] == '-')
                return ieFullNamees.OrderByDescending(i => i.GetType()
                                                            .GetProperty(sortby.Substring(1))
                                                            .GetValue(i, null))
                                    .ToList().GetRange(from, limit);
            else
                return ieFullNamees.OrderBy(i => i.GetType()
                                                  .GetProperty(sortby.Substring(1))
                                                  .GetValue(i, null))
                                    .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<IeFullName> Get()
        {
            if (_context.IeFullNames.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.IeFullNames, from, limit, sortby);
            }
            else
                return new List<IeFullName>();
        }

        [HttpGet("{id}", Name = "GetIeFullName")]
        public IActionResult Get(long id)
        {
            var item = _context.IeFullNames.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IeFullName item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.IeFullNames.Count() + 1;
            _context.IeFullNames.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetIeFullName", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] IeFullName item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var ieFullName = _context.IeFullNames.FirstOrDefault(t => t.Id == id);
            if (ieFullName == null)
            {
                return NotFound();
            }

            ieFullName.Surname = item.Surname;
            ieFullName.Name = item.Name;
            ieFullName.Patronymic = item.Patronymic;
            ieFullName.Sex = item.Sex;
            ieFullName.Grn = item.Grn;
            ieFullName.EntryDateIntEgrip = item.EntryDateIntEgrip;
            ieFullName.RegistrationAuthorityName = item.RegistrationAuthorityName;
            ieFullName.IndividualEntrepreneurId = item.IndividualEntrepreneurId;

            _context.IeFullNames.Update(ieFullName);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ieFullName = _context.IeFullNames.FirstOrDefault(t => t.Id == id);
            if (ieFullName == null)
            {
                return NotFound();
            }

            _context.IeFullNames.Remove(ieFullName);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}