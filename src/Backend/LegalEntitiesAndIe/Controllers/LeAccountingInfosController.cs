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
    public class LeAccountingInfosController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public LeAccountingInfosController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<LeAccountingInfo> Sort(DbSet<LeAccountingInfo> leAccountingInfoes, int from, int limit, string sortby)
        {
            if (typeof(LeAccountingInfo).GetProperty(sortby.Substring(1)) == null)
                return new List<LeAccountingInfo>();
            else if (sortby[0] == '-')
                return leAccountingInfoes.OrderByDescending(i => i.GetType()
                                                                  .GetProperty(sortby.Substring(1))
                                                                  .GetValue(i, null))
                                         .ToList().GetRange(from, limit);
            else
                return leAccountingInfoes.OrderBy(i => i.GetType()
                                                        .GetProperty(sortby.Substring(1))
                                                        .GetValue(i, null))
                                         .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<LeAccountingInfo> Get()
        {
            if (_context.LeAccountingInfos.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.LeAccountingInfos, from, limit, sortby);
            }
            else
                return new List<LeAccountingInfo>();
        }

        [HttpGet("{id}", Name = "GetLeAccountingInfo")]
        public IActionResult Get(long id)
        {
            var item = _context.LeAccountingInfos.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] LeAccountingInfo item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.LeAccountingInfos.Count() + 1;
            _context.LeAccountingInfos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetLeAccountingInfo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] LeAccountingInfo item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var leAccountingInfo = _context.LeAccountingInfos.FirstOrDefault(t => t.Id == id);
            if (leAccountingInfo == null)
            {
                return NotFound();
            }

            leAccountingInfo.Inn = item.Inn;
            leAccountingInfo.Grn = item.Grn;
            leAccountingInfo.Kpp = item.Kpp;
            leAccountingInfo.RegistationDate = item.RegistationDate;
            leAccountingInfo.TaxAuthorityName = item.TaxAuthorityName;
            leAccountingInfo.EntryDateInEgrul = item.EntryDateInEgrul;
            leAccountingInfo.LegalEntityId = item.LegalEntityId;

            _context.LeAccountingInfos.Update(leAccountingInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var leAccountingInfo = _context.LeAccountingInfos.FirstOrDefault(t => t.Id == id);
            if (leAccountingInfo == null)
            {
                return NotFound();
            }

            _context.LeAccountingInfos.Remove(leAccountingInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}