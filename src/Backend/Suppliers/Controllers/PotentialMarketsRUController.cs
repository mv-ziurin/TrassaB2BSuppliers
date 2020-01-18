using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Database;
using Suppliers.Database.Models;
using Microsoft.EntityFrameworkCore;


namespace Suppliers.Controllers
{
    [Route("api/[controller]")]
    public class PotentialMarketsRUController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public PotentialMarketsRUController(SuppliersContext context)
        {
            _context = context;
        }

        public List<PotentialMarketRU> Sort(DbSet<PotentialMarketRU> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<PotentialMarketRU>();
            else if (sortby[0] == '-')
                return Brandes.OrderByDescending(a => a.GetType()
                                                         .GetProperty(sortby.Substring(1))
                                                         .GetValue(a, null))
                                .ToList().GetRange(from, limit);
            else
                return Brandes.OrderBy(a => a.GetType()
                                               .GetProperty(sortby.Substring(1))
                                               .GetValue(a, null))
                                .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<PotentialMarketRU> Get()
        {
            if (_context.PotentialMarketsRU.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.PotentialMarketsRU, from, limit, sortby);
            }
            else
                return new List<PotentialMarketRU>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.PotentialMarketsRU.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PotentialMarketRU item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.PotentialMarketsRU.Count() + 1;
            _context.PotentialMarketsRU.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] PotentialMarketRU item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var market = _context.PotentialMarketsRU.FirstOrDefault(t => t.Id == id);
            if (market == null)
            {
                return NotFound();
            }

            market.BrandId = item.BrandId;
            market.ConsumerProfileId = item.ConsumerProfileId;
            market.CategoryTreeId = item.CategoryTreeId;
            market.Year = item.Year;
            market.NumberOfPotentialConsumers = item.NumberOfPotentialConsumers;
            market.AveragePurchaseSize = item.AveragePurchaseSize;
            market.AnnualPurchaseFrequency = item.AnnualPurchaseFrequency;
            market.PotentialAnnualMarketRU = item.PotentialAnnualMarketRU;

            _context.PotentialMarketsRU.Update(market);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var market = _context.PotentialMarketsRU.FirstOrDefault(t => t.Id == id);
            if (market == null)
            {
                return NotFound();
            }

            _context.PotentialMarketsRU.Remove(market);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}


