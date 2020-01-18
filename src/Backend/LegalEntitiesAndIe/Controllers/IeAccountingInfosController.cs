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
    public class IeAccountingInfosController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public IeAccountingInfosController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<IeAccountingInfo> Sort(DbSet<IeAccountingInfo> ieAccountingInfoes, int from, int limit, string sortby)
        {
            if (typeof(IeAccountingInfo).GetProperty(sortby.Substring(1)) == null)
                return new List<IeAccountingInfo>();
            else if (sortby[0] == '-')
                return ieAccountingInfoes.OrderByDescending(i => i.GetType()
                                                                  .GetProperty(sortby.Substring(1))
                                                                  .GetValue(i, null))
                                         .ToList().GetRange(from, limit);
            else
                return ieAccountingInfoes.OrderBy(i => i.GetType()
                                                        .GetProperty(sortby.Substring(1))
                                                        .GetValue(i, null))
                                         .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<IeAccountingInfo> Get()
        {
            if (_context.IeAccountingInfos.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.IeAccountingInfos, from, limit, sortby);
            }
            else
                return new List<IeAccountingInfo>();
        }

        [HttpGet("{id}", Name = "GetIeAccountingInfo")]
        public IActionResult Get(long id)
        {
            var item = _context.IeAccountingInfos.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IeAccountingInfo item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.IeAccountingInfos.Count() + 1;
            _context.IeAccountingInfos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetIeAccountingInfo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] IeAccountingInfo item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var ieAccountingInfo = _context.IeAccountingInfos.FirstOrDefault(t => t.Id == id);
            if (ieAccountingInfo == null)
            {
                return NotFound();
            }

            ieAccountingInfo.Inn = item.Inn;
            ieAccountingInfo.EntryDate = item.EntryDate;
            ieAccountingInfo.TaxAuthorityName = item.TaxAuthorityName;
            ieAccountingInfo.Grn = item.Grn;
            ieAccountingInfo.EntryDateInEgrip = item.EntryDateInEgrip;
            ieAccountingInfo.RegistrationAuthorityName = item.RegistrationAuthorityName;
            ieAccountingInfo.IndividualEntrepreneurId = item.IndividualEntrepreneurId;

            _context.IeAccountingInfos.Update(ieAccountingInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ieAccountingInfo = _context.IeAccountingInfos.FirstOrDefault(t => t.Id == id);
            if (ieAccountingInfo == null)
            {
                return NotFound();
            }

            _context.IeAccountingInfos.Remove(ieAccountingInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}