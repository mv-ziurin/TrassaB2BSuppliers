using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LegalWorkers.Database;
using LegalWorkers.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LegalWorkers.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly LegalWorkersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public PostsController(LegalWorkersContext context)
        {
            _context = context;
        }

        public List<Post> Sort(DbSet<Post> post, int from, int limit, string sortby)
        {
            if (typeof(Post).GetProperty(sortby.Substring(1)) == null)
                return new List<Post>();
            else if (sortby[0] == '-')
                return post.OrderByDescending(c => c.GetType()
                                                    .GetProperty(sortby.Substring(1))
                                                    .GetValue(c, null))
                           .ToList().GetRange(from, limit);
            else
                return post.OrderBy(c => c.GetType()
                                          .GetProperty(sortby.Substring(1))
                                          .GetValue(c, null))
                           .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            if (_context.EmployeeFunctions.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Posts, from, limit, sortby);
            }
            else
                return new List<Post>();
        }

        [HttpGet("{id}", Name = "GetPost")]
        public IActionResult Get(long id)
        {
            var item = _context.Posts.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Post item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Posts.Count() + 1;
            _context.Posts.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetPost", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Post item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var post = _context.Posts.FirstOrDefault(t => t.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            post.SectionId = item.SectionId;
            post.Name = item.Name;
            post.Description = item.Description;

            _context.Posts.Update(post);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var post = _context.Posts.FirstOrDefault(t => t.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
