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
    public class BrandSalesChannelsController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public BrandSalesChannelsController(SuppliersContext context)
        {
            _context = context;
        }

        public List<BrandSalesChannel> Sort(DbSet<BrandSalesChannel> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<BrandSalesChannel>();
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
        public IEnumerable<BrandSalesChannel> Get()
        {
            if (_context.BrandSalesChannels.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.BrandSalesChannels, from, limit, sortby);
            }
            else
                return new List<BrandSalesChannel>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.BrandSalesChannels.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BrandSalesChannel item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.BrandSalesChannels.Count() + 1;
            _context.BrandSalesChannels.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] BrandSalesChannel item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var channel = _context.BrandSalesChannels.FirstOrDefault(t => t.Id == id);
            if (channel == null)
            {
                return NotFound();
            }

            channel.BrandId = item.BrandId;
            channel.CategoryTreeId = item.CategoryTreeId;
            channel.TargetMarketSegment = item.TargetMarketSegment;

            _context.BrandSalesChannels.Update(channel);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var channel = _context.BrandSalesChannels.FirstOrDefault(t => t.Id == id);
            if (channel == null)
            {
                return NotFound();
            }

            _context.BrandSalesChannels.Remove(channel);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}


