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
    public class GroupNamesController : Controller
    {
        private readonly CounterpartiesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public GroupNamesController(CounterpartiesContext context)
        {
            _context = context;
        }

        public List<GroupName> Sort(DbSet<GroupName> groupNames, int from, int limit, string sortby)
        {
            if (typeof(GroupName).GetProperty(sortby.Substring(1)) == null)
                return new List<GroupName>();
            else if (sortby[0] == '-')
                return groupNames.OrderByDescending(a => a.GetType()
                                                          .GetProperty(sortby.Substring(1))
                                                          .GetValue(a, null))
                                 .ToList().GetRange(from, limit);
            else
                return groupNames.OrderBy(a => a.GetType()
                                                .GetProperty(sortby.Substring(1))
                                                .GetValue(a, null))
                                 .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<GroupName> Get()
        {
            if (_context.GroupNames.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.GroupNames, from, limit, sortby);
            }
            else
                return new List<GroupName>();
        }

        [HttpGet("{id}", Name = "GetGroupName")]
        public IActionResult Get(long id)
        {
            var item = _context.GroupNames.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] GroupName item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.GroupNames.Count() + 1;
            _context.GroupNames.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetGroupName", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] GroupName item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var groupName = _context.GroupNames.FirstOrDefault(t => t.Id == id);
            if (groupName == null)
            {
                return NotFound();
            }

            groupName.Name = item.Name;

            _context.GroupNames.Update(groupName);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var groupName = _context.GroupNames.FirstOrDefault(t => t.Id == id);
            if (groupName == null)
            {
                return NotFound();
            }

            _context.GroupNames.Remove(groupName);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}