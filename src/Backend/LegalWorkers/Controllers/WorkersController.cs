using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LegalWorkers.Database;
using LegalWorkers.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalWorkers.Controllers
{
    [Route("api/[controller]")]
    public class WorkersController : Controller
    {
        private readonly LegalWorkersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public WorkersController(LegalWorkersContext context)
        {
            _context = context;
        }

        public List<Worker> Sort(DbSet<Worker> worker, int from, int limit, string sortby)
        {
            if (typeof(WorkerFunction).GetProperty(sortby.Substring(1)) == null)
                return new List<Worker>();
            else if (sortby[0] == '-')
                return worker.OrderByDescending(c => c.GetType()
                                                      .GetProperty(sortby.Substring(1))
                                                      .GetValue(c, null))
                             .ToList().GetRange(from, limit);
            else
                return worker.OrderBy(c => c.GetType()
                                            .GetProperty(sortby.Substring(1))
                                            .GetValue(c, null))
                             .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<Worker> Get()
        {
            if (_context.Workers != null)
                return _context.Workers.ToList();
            else
                return new List<Worker>();
        }

        [HttpGet("{id}", Name = "GetWorker")]
        public IActionResult Get(long id)
        {
            var item = _context.Workers.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Worker item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Workers.Count() + 1;
            _context.Workers.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetWorker", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Worker item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var worker = _context.Workers.FirstOrDefault(t => t.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            worker.NaturalPersonId= item.NaturalPersonId;
            worker.LegalEntityId = item.LegalEntityId;
            worker.PostId = item.PostId;
            worker.DepartmentId = item.DepartmentId;
            worker.DirectorId = item.DirectorId;

            _context.Workers.Update(worker);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var worker = _context.Workers.FirstOrDefault(t => t.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            _context.Workers.Remove(worker);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
