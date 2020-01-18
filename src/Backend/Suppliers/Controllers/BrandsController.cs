using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Database;
using Suppliers.Database.Models;
using Microsoft.EntityFrameworkCore;


namespace Suppliers.Controllers
{
    [Route("api/[controller]")]
    public class BrandsController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public BrandsController(SuppliersContext context)
        {
            _context = context;
        }

        public List<Brand> Sort(DbSet<Brand> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<Brand>();
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
        public IEnumerable<Brand> Get()
        {
            if (_context.Brands.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Brands, from, limit, sortby);
            }
            else
                return new List<Brand>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.Brands.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Brand item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Brands.Count() + 1;
            _context.Brands.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Brand item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var brand = _context.Brands.FirstOrDefault(t => t.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            brand.CounteragentId = item.CounteragentId;
            brand.ResponsibleEmployeeId = item.ResponsibleEmployeeId;
            brand.TargetMarketSegmentRU = item.TargetMarketSegmentRU;
            brand.YearOfDistributionRU = item.YearOfDistributionRU;
            brand.DistributionModelRU = item.DistributionModelRU;
            brand.CategoryTreeId = item.CategoryTreeId;
            brand.DistributionDealSide = item.DistributionDealSide;
            brand.SupplyDealSide = item.SupplyDealSide;

            _context.Brands.Update(brand);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var brand = _context.Brands.FirstOrDefault(t => t.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}

