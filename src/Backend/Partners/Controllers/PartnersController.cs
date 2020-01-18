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
    public class PartnersController : Controller
    {
        private readonly PartnersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public PartnersController(PartnersContext context)
        {
            _context = context;
        }

        public List<Partner> Sort(DbSet<Partner> partners, int from, int limit, string sortby)
        {
            if (typeof(Partner).GetProperty(sortby.Substring(1)) == null)
                return new List<Partner>();
            else if (sortby[0] == '-')
                return partners.OrderByDescending(a => a.GetType()
                                                        .GetProperty(sortby.Substring(1))
                                                        .GetValue(a, null))
                               .ToList().GetRange(from, limit);
            else
                return partners.OrderBy(a => a.GetType()
                                              .GetProperty(sortby.Substring(1))
                                              .GetValue(a, null))
                               .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<Partner> Get()
        {
            if (_context.Partners.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Partners, from, limit, sortby);
            }
            else
                return new List<Partner>();
        }

        [HttpGet("{id}", Name = "GetPartner")]
        public IActionResult Get(long id)
        {
            var item = _context.Partners.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Partner item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Partners.Count() + 1;
            _context.Partners.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetPartner", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Partner item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var partner = _context.Partners.FirstOrDefault(t => t.Id == id);
            if (partner == null)
            {
                return NotFound();
            }

            partner.AddressId = item.AddressId;
            partner.AssortmentPercent = item.AssortmentPercent;
            partner.CounterpartyId = item.CounterpartyId;
            partner.CreationDate = item.CreationDate;
            partner.DiscountGoodsPercent = item.DiscountGoodsPercent;
            partner.Net = item.Net;
            partner.OfficialSite = item.OfficialSite;
            partner.OnlineSalesPercent = item.OnlineSalesPercent;
            partner.PriceSegment = item.PriceSegment;
            partner.PurchasingDecisionModelId = item.PurchasingDecisionModelId;
            partner.Repetition = item.Repetition;
            partner.Specialization = item.Specialization;
            partner.Vip = item.Vip;
            partner.WriterId = item.WriterId;

            _context.Partners.Update(partner);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var partner = _context.Partners.FirstOrDefault(t => t.Id == id);
            if (partner == null)
            {
                return NotFound();
            }

            _context.Partners.Remove(partner);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}