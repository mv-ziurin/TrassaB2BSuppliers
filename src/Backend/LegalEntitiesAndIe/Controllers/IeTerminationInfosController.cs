using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LegalEntitiesAndIe.Database.Models;
using LegalEntitiesAndIe.Database;

namespace LegalEntitiesAndIe.Controllers
{
    [Route("api/[controller]")]
    public class IeTerminationInfosController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public IeTerminationInfosController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<IeTerminationInfo> Sort(DbSet<IeTerminationInfo> ieTerminationInfoes, int from, int limit, string sortby)
        {
            if (typeof(IeTerminationInfo).GetProperty(sortby.Substring(1)) == null)
                return new List<IeTerminationInfo>();
            else if (sortby[0] == '-')
                return ieTerminationInfoes.OrderByDescending(i => i.GetType()
                                                                   .GetProperty(sortby.Substring(1))
                                                                   .GetValue(i, null))
                                          .ToList().GetRange(from, limit);
            else
                return ieTerminationInfoes.OrderBy(i => i.GetType()
                                                         .GetProperty(sortby.Substring(1))
                                                         .GetValue(i, null))
                                          .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<IeTerminationInfo> Get()
        {
            if (_context.IeTerminationInfos.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.IeTerminationInfos, from, limit, sortby);
            }
            else
                return new List<IeTerminationInfo>();
        }

        [HttpGet("{id}", Name = "GetIeTerminationInfo")]
        public IActionResult Get(long id)
        {
            var item = _context.IeTerminationInfos.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IeTerminationInfo item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.IeTerminationInfos.Count() + 1;
            _context.IeTerminationInfos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetIeTerminationInfo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] IeTerminationInfo item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var ieTerminationInfo = _context.IeTerminationInfos.FirstOrDefault(t => t.Id == id);
            if (ieTerminationInfo == null)
            {
                return NotFound();
            }


            ieTerminationInfo.TerminationMethod = item.TerminationMethod;
            ieTerminationInfo.TerminationDate = item.TerminationDate;
            ieTerminationInfo.Grn = item.Grn;
            ieTerminationInfo.EntryDateInEgrip = item.EntryDateInEgrip;
            ieTerminationInfo.RegistrationAuthorityName = item.RegistrationAuthorityName;
            ieTerminationInfo.IndividualEntrepreneurId = item.IndividualEntrepreneurId;

            _context.IeTerminationInfos.Update(ieTerminationInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ieTerminationInfo = _context.IeTerminationInfos.FirstOrDefault(t => t.Id == id);
            if (ieTerminationInfo == null)
            {
                return NotFound();
            }

            _context.IeTerminationInfos.Remove(ieTerminationInfo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}