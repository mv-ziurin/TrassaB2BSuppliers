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
    public class LeAuthorityInfosController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public LeAuthorityInfosController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<LeAuthorityInfo> Sort(DbSet<LeAuthorityInfo> leAuthorityInfoes, int from, int limit, string sortby)
        {
            if (typeof(LeAuthorityInfo).GetProperty(sortby.Substring(1)) == null)
                return new List<LeAuthorityInfo>();
            else if (sortby[0] == '-')
                return leAuthorityInfoes.OrderByDescending(i => i.GetType()
                                                                 .GetProperty(sortby.Substring(1))
                                                                 .GetValue(i, null))
                                        .ToList().GetRange(from, limit);
            else
                return leAuthorityInfoes.OrderBy(i => i.GetType()
                                                       .GetProperty(sortby.Substring(1))
                                                       .GetValue(i, null))
                                        .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<LeAuthorityInfo> Get()
        {
            if (_context.LeAuthorityInfos.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.LeAuthorityInfos, from, limit, sortby);
            }
            else
                return new List<LeAuthorityInfo>();
        }

        [HttpGet("{id}", Name = "GetLeAuthorityInfo")]
        public IActionResult Get(long id)
        {
            var item = _context.LeAuthorityInfos.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] LeAuthorityInfo item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.LeAuthorityInfos.Count() + 1;
            _context.LeAuthorityInfos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetLeAuthorityInfo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] LeAuthorityInfo item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var leAuthorityInfo = _context.LeAuthorityInfos.FirstOrDefault(t => t.Id == id);
            if (leAuthorityInfo == null)
            {
                return NotFound();
            }

            leAuthorityInfo.EntryDateInEgrul = item.EntryDateInEgrul;
            leAuthorityInfo.Grn = item.Grn;
            leAuthorityInfo.Name = item.Name;
            leAuthorityInfo.AddressId = item.AddressId;
            leAuthorityInfo.LegalEntityId = item.LegalEntityId;

            _context.LeAuthorityInfos.Update(leAuthorityInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var LeAuthorityInfo = _context.LeAuthorityInfos.FirstOrDefault(t => t.Id == id);
            if (LeAuthorityInfo == null)
            {
                return NotFound();
            }

            _context.LeAuthorityInfos.Remove(LeAuthorityInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}