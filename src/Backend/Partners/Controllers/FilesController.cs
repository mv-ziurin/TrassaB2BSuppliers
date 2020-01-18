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
    public class FilesController : Controller
    {
        private readonly PartnersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public FilesController(PartnersContext context)
        {
            _context = context;
        }

        public List<File> Sort(DbSet<File> files, int from, int limit, string sortby)
        {
            if (typeof(File).GetProperty(sortby.Substring(1)) == null)
                return new List<File>();
            else if (sortby[0] == '-')
                return files.OrderByDescending(a => a.GetType()
                                                     .GetProperty(sortby.Substring(1))
                                                     .GetValue(a, null))
                            .ToList().GetRange(from, limit);
            else
                return files.OrderBy(a => a.GetType()
                                           .GetProperty(sortby.Substring(1))
                                           .GetValue(a, null))
                            .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<File> Get()
        {
            if (_context.Files.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Files, from, limit, sortby);
            }
            else
                return new List<File>();
        }

        [HttpGet("{id}", Name = "GetFile")]
        public IActionResult Get(long id)
        {
            var item = _context.Files.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] File item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Files.Count() + 1;
            _context.Files.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetFile", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] File item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var file = _context.Files.FirstOrDefault(t => t.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            file.CreationDate = item.CreationDate;
            file.FileSedId = item.FileSedId;
            file.Name = item.Name;
            file.Size = item.Size;
            file.Type = item.Type;

            _context.Files.Update(file);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var file = _context.Files.FirstOrDefault(t => t.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            _context.Files.Remove(file);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}