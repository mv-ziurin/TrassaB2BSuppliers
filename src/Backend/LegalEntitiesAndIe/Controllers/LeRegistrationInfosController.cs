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
    public class LeRegistrationInfosController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public LeRegistrationInfosController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<LeRegistrationInfo> Sort(DbSet<LeRegistrationInfo> leRegistrationInfo, int from, int limit, string sortby)
        {
            if (typeof(LeRegistrationInfo).GetProperty(sortby.Substring(1)) == null)
                return new List<LeRegistrationInfo>();
            else if (sortby[0] == '-')
                return leRegistrationInfo.OrderByDescending(i => i.GetType()
                                                                  .GetProperty(sortby.Substring(1))
                                                                  .GetValue(i, null))
                                         .ToList().GetRange(from, limit);
            else
                return leRegistrationInfo.OrderBy(i => i.GetType()
                                                        .GetProperty(sortby.Substring(1))
                                                        .GetValue(i, null))
                                         .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<LeRegistrationInfo> Get()
        {
            if (_context.LeRegistrationInfos.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.LeRegistrationInfos, from, limit, sortby);
            }
            else
                return new List<LeRegistrationInfo>();
        }

        [HttpGet("{id}", Name = "GetLeRegistrationInfo")]
        public IActionResult Get(long id)
        {
            var item = _context.LeRegistrationInfos.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] LeRegistrationInfo item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.LeRegistrationInfos.Count() + 1;
            _context.LeRegistrationInfos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetLeRegistrationInfo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] LeRegistrationInfo item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var leRegistrationInfo = _context.LeRegistrationInfos.FirstOrDefault(t => t.Id == id);
            if (leRegistrationInfo == null)
            {
                return NotFound();
            }

            leRegistrationInfo.AssignmentDateOfOgrn = item.AssignmentDateOfOgrn;
            leRegistrationInfo.EntryDateInEgrul = item.EntryDateInEgrul;
            leRegistrationInfo.Grn = item.Grn;
            leRegistrationInfo.LegalEntityId = item.LegalEntityId;
            leRegistrationInfo.Ogrn = item.Ogrn;
            leRegistrationInfo.OldRegistrationAuthority = item.OldRegistrationAuthority;
            leRegistrationInfo.OldRegistrationNumber = item.OldRegistrationNumber;

            _context.LeRegistrationInfos.Update(leRegistrationInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var LeRegistrationInfo = _context.LeRegistrationInfos.FirstOrDefault(t => t.Id == id);
            if (LeRegistrationInfo == null)
            {
                return NotFound();
            }

            _context.LeRegistrationInfos.Remove(LeRegistrationInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}