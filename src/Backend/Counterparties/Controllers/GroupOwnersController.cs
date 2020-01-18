using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Counterparties.Database;
using Counterparties.Database.Models;

namespace Counterparties.Controllers
{
    [Route("api/[controller]")]
    public class GroupOwnersController : Controller
    {
        private readonly CounterpartiesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public GroupOwnersController(CounterpartiesContext context)
        {
            _context = context;
        }

        public List<GroupOwner> Sort(DbSet<GroupOwner> groupOwners, int from, int limit, string sortby)
        {
            if (typeof(GroupOwner).GetProperty(sortby.Substring(1)) == null)
                return new List<GroupOwner>();
            else if (sortby[0] == '-')
                return groupOwners.OrderByDescending(a => a.GetType()
                                                           .GetProperty(sortby.Substring(1))
                                                           .GetValue(a, null))
                                  .ToList().GetRange(from, limit);
            else
                return groupOwners.OrderBy(a => a.GetType()
                                                 .GetProperty(sortby.Substring(1))
                                                 .GetValue(a, null))
                                  .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<GroupOwner> Get()
        {
            if (_context.GroupOwners.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.GroupOwners, from, limit, sortby);
            }
            else
                return new List<GroupOwner>();
        }

        [HttpGet("{id}", Name = "GetGroupOwner")]
        public IActionResult Get(long id)
        {
            var item = _context.GroupOwners.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] GroupOwner item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.GroupOwners.Count() + 1;
            _context.GroupOwners.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetGroupOwner", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] GroupOwner item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var groupOwner = _context.GroupOwners.FirstOrDefault(t => t.Id == id);
            if (groupOwner == null)
            {
                return NotFound();
            }

            groupOwner.NaturalPersonId = item.NaturalPersonId;

            _context.GroupOwners.Update(groupOwner);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var groupOwner = _context.GroupOwners.FirstOrDefault(t => t.Id == id);
            if (groupOwner == null)
            {
                return NotFound();
            }

            _context.GroupOwners.Remove(groupOwner);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}