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
    public class FilialPhotosController : Controller
    {
        private readonly CounterpartiesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public FilialPhotosController(CounterpartiesContext context)
        {
            _context = context;
        }

        public List<FilialPhoto> Sort(DbSet<FilialPhoto> filialPhotos, int from, int limit, string sortby)
        {
            if (typeof(FilialPhoto).GetProperty(sortby.Substring(1)) == null)
                return new List<FilialPhoto>();
            else if (sortby[0] == '-')
                return filialPhotos.OrderByDescending(a => a.GetType()
                                                            .GetProperty(sortby.Substring(1))
                                                            .GetValue(a, null))
                                   .ToList().GetRange(from, limit);
            else
                return filialPhotos.OrderBy(a => a.GetType()
                                                  .GetProperty(sortby.Substring(1))
                                                  .GetValue(a, null))
                                   .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<FilialPhoto> Get()
        {
            if (_context.FilialPhotos.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.FilialPhotos, from, limit, sortby);
            }
            else
                return new List<FilialPhoto>();
        }

        [HttpGet("{id}", Name = "GetFilialPhoto")]
        public IActionResult Get(long id)
        {
            var item = _context.FilialPhotos.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] FilialPhoto item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.FilialPhotos.Count() + 1;
            _context.FilialPhotos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetFilialPhoto", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] FilialPhoto item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var filialPhoto = _context.FilialPhotos.FirstOrDefault(t => t.Id == id);
            if (filialPhoto == null)
            {
                return NotFound();
            }

            filialPhoto.FileId = item.FileId;
            filialPhoto.FilialId = item.FilialId;

            _context.FilialPhotos.Update(filialPhoto);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var filialPhoto = _context.FilialPhotos.FirstOrDefault(t => t.Id == id);
            if (filialPhoto == null)
            {
                return NotFound();
            }

            _context.FilialPhotos.Remove(filialPhoto);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}