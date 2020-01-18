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
    public class IeAuthorityInfosController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public IeAuthorityInfosController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<IeAuthorityInfo> Sort(DbSet<IeAuthorityInfo> ieAuthorityInfoes, int from, int limit, string sortby)
        {
            if (typeof(IeAuthorityInfo).GetProperty(sortby.Substring(1)) == null)
                return new List<IeAuthorityInfo>();
            else if (sortby[0] == '-')
                return ieAuthorityInfoes.OrderByDescending(i => i.GetType()
                                                                 .GetProperty(sortby.Substring(1))
                                                                 .GetValue(i, null))
                                        .ToList().GetRange(from, limit);
            else
                return ieAuthorityInfoes.OrderBy(i => i.GetType()
                                                       .GetProperty(sortby.Substring(1))
                                                       .GetValue(i, null))
                                        .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<IeAuthorityInfo> Get()
        {
            if (_context.IeAuthorityInfos.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.IeAuthorityInfos, from, limit, sortby);
            }
            else
                return new List<IeAuthorityInfo>();
        }

        [HttpGet("{id}", Name = "GetIeAuthorityInfo")]
        public IActionResult Get(long id)
        {
            var item = _context.IeAuthorityInfos.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IeAuthorityInfo item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.IeAuthorityInfos.Count() + 1;
            _context.IeAuthorityInfos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetIeAuthorityInfo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] IeAuthorityInfo item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var ieAuthorityInfo = _context.IeAuthorityInfos.FirstOrDefault(t => t.Id == id);
            if (ieAuthorityInfo == null)
            {
                return NotFound();
            }

            ieAuthorityInfo.RegistrationAuthorityName = item.RegistrationAuthorityName;
            ieAuthorityInfo.RegistrationAuthorityId = item.RegistrationAuthorityId;
            ieAuthorityInfo.Patronymic = item.Patronymic;
            ieAuthorityInfo.Grn = item.Grn;
            ieAuthorityInfo.EntryDateIntEgrip = item.EntryDateIntEgrip;
            ieAuthorityInfo.IndividualEntrepreneurId = item.IndividualEntrepreneurId;

            _context.IeAuthorityInfos.Update(ieAuthorityInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ieAuthorityInfo = _context.IeAuthorityInfos.FirstOrDefault(t => t.Id == id);
            if (ieAuthorityInfo == null)
            {
                return NotFound();
            }

            _context.IeAuthorityInfos.Remove(ieAuthorityInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}