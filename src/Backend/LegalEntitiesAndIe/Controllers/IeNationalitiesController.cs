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
    public class IeNationalitiesController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public IeNationalitiesController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<IeNationality> Sort(DbSet<IeNationality> ieNationalityes, int from, int limit, string sortby)
        {
            if (typeof(IeNationality).GetProperty(sortby.Substring(1)) == null)
                return new List<IeNationality>();
            else if (sortby[0] == '-')
                return ieNationalityes.OrderByDescending(i => i.GetType()
                                                               .GetProperty(sortby.Substring(1))
                                                               .GetValue(i, null))
                                      .ToList().GetRange(from, limit);
            else
                return ieNationalityes.OrderBy(i => i.GetType()
                                                     .GetProperty(sortby.Substring(1))
                                                     .GetValue(i, null))
                                      .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<IeNationality> Get()
        {
            if (_context.IeNationalities.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.IeNationalities, from, limit, sortby);
            }
            else
                return new List<IeNationality>();
        }

        [HttpGet("{id}", Name = "GetIeNationality")]
        public IActionResult Get(long id)
        {
            var item = _context.IeNationalities.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IeNationality item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.IeNationalities.Count() + 1;
            _context.IeNationalities.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetIeNationality", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] IeNationality item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var ieNationality = _context.IeNationalities.FirstOrDefault(t => t.Id == id);
            if (ieNationality == null)
            {
                return NotFound();
            }

            ieNationality.Nationality = item.Nationality;
            ieNationality.Grn = item.Grn;
            ieNationality.EntryDateInEgrip = item.EntryDateInEgrip;
            ieNationality.RegistrationAuthorityName = item.RegistrationAuthorityName;
            ieNationality.IndividualEntrepreneurId = item.IndividualEntrepreneurId;

            _context.IeNationalities.Update(ieNationality);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ieNationality = _context.IeNationalities.FirstOrDefault(t => t.Id == id);
            if (ieNationality == null)
            {
                return NotFound();
            }

            _context.IeNationalities.Remove(ieNationality);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}