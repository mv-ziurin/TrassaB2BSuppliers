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
    public class SalesPointPhotosController : Controller
    {
        private readonly PartnersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public SalesPointPhotosController(PartnersContext context)
        {
            _context = context;
        }

        public List<SalesPointPhoto> Sort(DbSet<SalesPointPhoto> salesPointPhotos, int from, int limit, string sortby)
        {
            if (typeof(SalesPointPhoto).GetProperty(sortby.Substring(1)) == null)
                return new List<SalesPointPhoto>();
            else if (sortby[0] == '-')
                return salesPointPhotos.OrderByDescending(a => a.GetType()
                                                                .GetProperty(sortby.Substring(1))
                                                                .GetValue(a, null))
                                       .ToList().GetRange(from, limit);
            else
                return salesPointPhotos.OrderBy(a => a.GetType()
                                                      .GetProperty(sortby.Substring(1))
                                                      .GetValue(a, null))
                                       .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<SalesPointPhoto> Get()
        {
            if (_context.SalesPointPhotos.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.SalesPointPhotos, from, limit, sortby);
            }
            else
                return new List<SalesPointPhoto>();
        }

        [HttpGet("{id}", Name = "GetSalesPointPhoto")]
        public IActionResult Get(long id)
        {
            var item = _context.SalesPointPhotos.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SalesPointPhoto item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.SalesPointPhotos.Count() + 1;
            _context.SalesPointPhotos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetSalesPointPhoto", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] SalesPointPhoto item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var salesPointPhoto = _context.SalesPointPhotos.FirstOrDefault(t => t.Id == id);
            if (salesPointPhoto == null)
            {
                return NotFound();
            }

            salesPointPhoto.FileId = item.FileId;
            salesPointPhoto.SalesPointId = item.SalesPointId;
            salesPointPhoto.Type = item.Type;

            _context.SalesPointPhotos.Update(salesPointPhoto);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var SalesPointPhoto = _context.SalesPointPhotos.FirstOrDefault(t => t.Id == id);
            if (SalesPointPhoto == null)
            {
                return NotFound();
            }

            _context.SalesPointPhotos.Remove(SalesPointPhoto);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}