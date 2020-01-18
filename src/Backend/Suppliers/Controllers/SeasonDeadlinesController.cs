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
    public class SeasonDeadlinesController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public SeasonDeadlinesController(SuppliersContext context)
        {
            _context = context;
        }

        public List<SeasonDeadline> Sort(DbSet<SeasonDeadline> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<SeasonDeadline>();
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
        public IEnumerable<SeasonDeadline> Get()
        {
            if (_context.SeasonDeadlines.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.SeasonDeadlines, from, limit, sortby);
            }
            else
                return new List<SeasonDeadline>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.SeasonDeadlines.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SeasonDeadline item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.SeasonDeadlines.Count() + 1;
            _context.SeasonDeadlines.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] SeasonDeadline item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var deadline = _context.SeasonDeadlines.FirstOrDefault(t => t.Id == id);
            if (deadline == null)
            {
                return NotFound();
            }

            deadline.SeasonId = item.SeasonId;
            deadline.DeadlineNumber = item.DeadlineNumber;
            deadline.DateOfPartnerPreorderDeadline = item.DateOfPartnerPreorderDeadline;
            deadline.DateOfCurrentDeadlineBrandManagerPreorderCheck = item.DateOfCurrentDeadlineBrandManagerPreorderCheck;
            deadline.DateOfCurrentDeadlineWarehouseProgramOrderOffer = item.DateOfCurrentDeadlineWarehouseProgramOrderOffer;
            deadline.DateOfCurrentDeadlineOrderProductCommiteeReconciliation = item.DateOfCurrentDeadlineOrderProductCommiteeReconciliation;
            deadline.DateOfCurrentDeadlineOrderPost = item.DateOfCurrentDeadlineOrderPost;
            deadline.DateOfCurrentDeadlineDeliveryReadyness = item.DateOfCurrentDeadlineDeliveryReadyness;
            deadline.DateOfCurrentDeadlineWarehouseOrderReceipt = item.DateOfCurrentDeadlineWarehouseOrderReceipt;

            _context.SeasonDeadlines.Update(deadline);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var deadline = _context.SeasonDeadlines.FirstOrDefault(t => t.Id == id);
            if (deadline == null)
            {
                return NotFound();
            }

            _context.SeasonDeadlines.Remove(deadline);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}


