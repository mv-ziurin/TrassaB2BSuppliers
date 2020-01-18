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
    public class SocialNetworksController : Controller
    {
        private readonly PartnersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public SocialNetworksController(PartnersContext context)
        {
            _context = context;
        }

        public List<SocialNetwork> Sort(DbSet<SocialNetwork> socialNetworks, int from, int limit, string sortby)
        {
            if (typeof(SocialNetwork).GetProperty(sortby.Substring(1)) == null)
                return new List<SocialNetwork>();
            else if (sortby[0] == '-')
                return socialNetworks.OrderByDescending(a => a.GetType()
                                                              .GetProperty(sortby.Substring(1))
                                                              .GetValue(a, null))
                                     .ToList().GetRange(from, limit);
            else
                return socialNetworks.OrderBy(a => a.GetType()
                                                    .GetProperty(sortby.Substring(1))
                                                    .GetValue(a, null))
                                     .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<SocialNetwork> Get()
        {
            if (_context.SocialNetworks.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.SocialNetworks, from, limit, sortby);
            }
            else
                return new List<SocialNetwork>();
        }

        [HttpGet("{id}", Name = "GetSocialNetwork")]
        public IActionResult Get(long id)
        {
            var item = _context.SocialNetworks.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SocialNetwork item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.SocialNetworks.Count() + 1;
            _context.SocialNetworks.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetSocialNetwork", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] SocialNetwork item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var socialNetwork = _context.SocialNetworks.FirstOrDefault(t => t.Id == id);
            if (socialNetwork == null)
            {
                return NotFound();
            }

            socialNetwork.Name = item.Name;
            socialNetwork.URL = item.URL;

            _context.SocialNetworks.Update(socialNetwork);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var socialNetwork = _context.SocialNetworks.FirstOrDefault(t => t.Id == id);
            if (socialNetwork == null)
            {
                return NotFound();
            }

            _context.SocialNetworks.Remove(socialNetwork);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}