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
    public class IeRegistrationInfosController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public IeRegistrationInfosController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<IeRegistrationInfo> Sort(DbSet<IeRegistrationInfo> ieRegistrationInfoes, int from, int limit, string sortby)
        {
            if (typeof(IeRegistrationInfo).GetProperty(sortby.Substring(1)) == null)
                return new List<IeRegistrationInfo>();
            else if (sortby[0] == '-')
                return ieRegistrationInfoes.OrderByDescending(i => i.GetType()
                                                                    .GetProperty(sortby.Substring(1))
                                                                    .GetValue(i, null))
                                           .ToList().GetRange(from, limit);
            else
                return ieRegistrationInfoes.OrderBy(i => i.GetType()
                                                          .GetProperty(sortby.Substring(1))
                                                          .GetValue(i, null))
                                            .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<IeRegistrationInfo> Get()
        {
            if (_context.IeRegistrationInfos.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.IeRegistrationInfos, from, limit, sortby);
            }
            else
                return new List<IeRegistrationInfo>();
        }

        [HttpGet("{id}", Name = "GetIeRegistrationInfo")]
        public IActionResult Get(long id)
        {
            var item = _context.IeRegistrationInfos.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IeRegistrationInfo item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.IeRegistrationInfos.Count() + 1;
            _context.IeRegistrationInfos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetIeRegistrationInfo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] IeRegistrationInfo item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var ieRegistrationInfo = _context.IeRegistrationInfos.FirstOrDefault(t => t.Id == id);
            if (ieRegistrationInfo == null)
            {
                return NotFound();
            }

            ieRegistrationInfo.Ogrnip = item.Ogrnip;
            ieRegistrationInfo.EntryDateInOgrnip = item.EntryDateInOgrnip;
            ieRegistrationInfo.OldRegistrationAuthority = item.OldRegistrationAuthority;
            ieRegistrationInfo.OldRegistrationDate = item.OldRegistrationDate;
            ieRegistrationInfo.OldRegistrationNumber = item.OldRegistrationNumber;
            ieRegistrationInfo.ReistrationAuthorityName = item.ReistrationAuthorityName;
            ieRegistrationInfo.IndividualEntrepreneurId = item.IndividualEntrepreneurId;

            _context.IeRegistrationInfos.Update(ieRegistrationInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ieRegistrationInfo = _context.IeRegistrationInfos.FirstOrDefault(t => t.Id == id);
            if (ieRegistrationInfo == null)
            {
                return NotFound();
            }

            _context.IeRegistrationInfos.Remove(ieRegistrationInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}