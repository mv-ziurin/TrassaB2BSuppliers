using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Partners.Database;
using Partners.Database.Models;

namespace Partners.Controllers
{
    [Route("api/[controller]")]
    public class PartnersContactDetailsController : Controller
    {
        private readonly PartnersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public PartnersContactDetailsController(PartnersContext context)
        {
            _context = context;
        }

        public List<PartnersContactDetail> Sort(DbSet<PartnersContactDetail> partnerContactDetails, int from, int limit, string sortby)
        {
            if (typeof(PartnersContactDetail).GetProperty(sortby.Substring(1)) == null)
                return new List<PartnersContactDetail>();
            else if (sortby[0] == '-')
                return partnerContactDetails.OrderByDescending(a => a.GetType()
                                                                     .GetProperty(sortby.Substring(1))
                                                                     .GetValue(a, null))
                                            .ToList().GetRange(from, limit);
            else
                return partnerContactDetails.OrderBy(a => a.GetType()
                                                           .GetProperty(sortby.Substring(1))
                                                           .GetValue(a, null))
                                            .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<PartnersContactDetail> Get()
        {
            if (_context.PartnerContactDetails.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.PartnerContactDetails, from, limit, sortby);
            }
            else
                return new List<PartnersContactDetail>();
        }

        [HttpGet("{id}", Name = "GetPartnerContactDetail")]
        public IActionResult Get(long id)
        {
            var item = _context.PartnerContactDetails.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PartnersContactDetail item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.PartnerContactDetails.Count() + 1;
            _context.PartnerContactDetails.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetPartnerContactDetail", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] PartnersContactDetail item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var partnerContactDetail = _context.PartnerContactDetails.FirstOrDefault(t => t.Id == id);
            if (partnerContactDetail == null)
            {
                return NotFound();
            }

            partnerContactDetail.ContactDetailId = item.ContactDetailId;
            partnerContactDetail.PartnerId = item.PartnerId;
            partnerContactDetail.SalesPointId = item.SalesPointId;

            _context.PartnerContactDetails.Update(partnerContactDetail);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var partnerContactDetail = _context.PartnerContactDetails.FirstOrDefault(t => t.Id == id);
            if (partnerContactDetail == null)
            {
                return NotFound();
            }

            _context.PartnerContactDetails.Remove(partnerContactDetail);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}