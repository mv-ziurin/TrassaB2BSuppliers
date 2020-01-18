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
    public class FilialCollaboratorsController : Controller
    {
        private readonly CounterpartiesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public FilialCollaboratorsController(CounterpartiesContext context)
        {
            _context = context;
        }

        public List<FilialCollaborator> Sort(DbSet<FilialCollaborator> filialCollaborators, int from, int limit, string sortby)
        {
            if (typeof(FilialCollaborator).GetProperty(sortby.Substring(1)) == null)
                return new List<FilialCollaborator>();
            else if (sortby[0] == '-')
                return filialCollaborators.OrderByDescending(a => a.GetType()
                                                                   .GetProperty(sortby.Substring(1))
                                                                   .GetValue(a, null))
                                          .ToList().GetRange(from, limit);
            else
                return filialCollaborators.OrderBy(a => a.GetType()
                                                         .GetProperty(sortby.Substring(1))
                                                         .GetValue(a, null))
                                          .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<FilialCollaborator> Get()
        {
            if (_context.FilialCollaborators.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.FilialCollaborators, from, limit, sortby);
            }
            else
                return new List<FilialCollaborator>();
        }

        [HttpGet("{id}", Name = "GetFilialCollaborator")]
        public IActionResult Get(long id)
        {
            var item = _context.FilialCollaborators.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] FilialCollaborator item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.FilialCollaborators.Count() + 1;
            _context.FilialCollaborators.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetFilialCollaborator", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] FilialCollaborator item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var filialCollaborator = _context.FilialCollaborators.FirstOrDefault(t => t.Id == id);
            if (filialCollaborator == null)
            {
                return NotFound();
            }

            filialCollaborator.CollaboratorId = item.CollaboratorId;
            filialCollaborator.FilialId = item.FilialId;

            _context.FilialCollaborators.Update(filialCollaborator);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var filialCollaborator = _context.FilialCollaborators.FirstOrDefault(t => t.Id == id);
            if (filialCollaborator == null)
            {
                return NotFound();
            }

            _context.FilialCollaborators.Remove(filialCollaborator);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}