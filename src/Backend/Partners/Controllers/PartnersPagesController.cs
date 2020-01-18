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
    public class PartnersPagesController : Controller
    {
        private readonly PartnersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public PartnersPagesController(PartnersContext context)
        {
            _context = context;
        }

        public List<PartnersPage> Sort(DbSet<PartnersPage> partnersPages, int from, int limit, string sortby)
        {
            if (typeof(PartnersPage).GetProperty(sortby.Substring(1)) == null)
                return new List<PartnersPage>();
            else if (sortby[0] == '-')
                return partnersPages.OrderByDescending(a => a.GetType()
                                                             .GetProperty(sortby.Substring(1))
                                                             .GetValue(a, null))
                                    .ToList().GetRange(from, limit);
            else
                return partnersPages.OrderBy(a => a.GetType()
                                                   .GetProperty(sortby.Substring(1))
                                                   .GetValue(a, null))
                                    .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<PartnersPage> Get()
        {
            if (_context.PartnersPages.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.PartnersPages, from, limit, sortby);
            }
            else
                return new List<PartnersPage>();
        }

        [HttpGet("{id}", Name = "GetPartnersPage")]
        public IActionResult Get(long id)
        {
            var item = _context.PartnersPages.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PartnersPage item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.PartnersPages.Count() + 1;
            _context.PartnersPages.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetPartnersPage", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] PartnersPage item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var partnersPage = _context.PartnersPages.FirstOrDefault(t => t.Id == id);
            if (partnersPage == null)
            {
                return NotFound();
            }

            partnersPage.PartnerId = item.PartnerId;
            partnersPage.SocialNetWorkId = item.SocialNetWorkId;
            partnersPage.URL = item.URL;

            _context.PartnersPages.Update(partnersPage);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var partnersPage = _context.PartnersPages.FirstOrDefault(t => t.Id == id);
            if (partnersPage == null)
            {
                return NotFound();
            }

            _context.PartnersPages.Remove(partnersPage);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}