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
    public class SalesPointsController : Controller
    {
        private readonly PartnersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public SalesPointsController(PartnersContext context)
        {
            _context = context;
        }

        public List<SalesPoint> Sort(DbSet<SalesPoint> salesPoints, int from, int limit, string sortby)
        {
            if (typeof(SalesPoint).GetProperty(sortby.Substring(1)) == null)
                return new List<SalesPoint>();
            else if (sortby[0] == '-')
                return salesPoints.OrderByDescending(a => a.GetType()
                                                           .GetProperty(sortby.Substring(1))
                                                           .GetValue(a, null))
                                  .ToList().GetRange(from, limit);
            else
                return salesPoints.OrderBy(a => a.GetType()
                                                 .GetProperty(sortby.Substring(1))
                                                 .GetValue(a, null))
                                  .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<SalesPoint> Get()
        {
            if (_context.SalesPoints.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.SalesPoints, from, limit, sortby);
            }
            else
                return new List<SalesPoint>();
        }

        [HttpGet("{id}", Name = "GetSalesPoint")]
        public IActionResult Get(long id)
        {
            var item = _context.SalesPoints.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SalesPoint item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.SalesPoints.Count() + 1;
            _context.SalesPoints.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetSalesPoint", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] SalesPoint item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var salesPoint = _context.SalesPoints.FirstOrDefault(t => t.Id == id);
            if (salesPoint == null)
            {
                return NotFound();
            }

            salesPoint.AddressId = item.AddressId;
            salesPoint.Age = item.Age;
            salesPoint.Area = item.Area;
            salesPoint.Author = item.Author;
            salesPoint.AverageBuyerTraffic = item.AverageBuyerTraffic;
            salesPoint.AverageCheck = item.AverageCheck;
            salesPoint.AverageMarkUp = item.AverageMarkUp;
            salesPoint.CategoryId = item.CategoryId;
            salesPoint.ChecksPerMonth = item.ChecksPerMonth;
            salesPoint.CollaboratorNumber = item.CollaboratorNumber;
            salesPoint.Name = item.Name;
            salesPoint.NumberOfSKU = item.NumberOfSKU;
            salesPoint.PertnerId = item.PertnerId;
            salesPoint.SalesPerYear = item.SalesPerYear;
            salesPoint.SalesWithTrassaPerYear = item.SalesWithTrassaPerYear;
            salesPoint.TargetMarketSegment = item.TargetMarketSegment;
            salesPoint.TrassaPenetration = item.TrassaPenetration;
            salesPoint.UpdateTime = item.UpdateTime;
            salesPoint.WorkingFormat = item.WorkingFormat;

            _context.SalesPoints.Update(salesPoint);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var salesPoint = _context.SalesPoints.FirstOrDefault(t => t.Id == id);
            if (salesPoint == null)
            {
                return NotFound();
            }

            _context.SalesPoints.Remove(salesPoint);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}