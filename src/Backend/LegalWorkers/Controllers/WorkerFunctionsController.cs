using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LegalWorkers.Database;
using LegalWorkers.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalWorkers.Controllers
{
    [Route("api/[controller]")]
    public class WorkerFunctionsController : Controller
    {
        private readonly LegalWorkersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public WorkerFunctionsController(LegalWorkersContext context)
        {
            _context = context;
        }

        public List<WorkerFunction> Sort(DbSet<WorkerFunction> workerFunction, int from, int limit, string sortby)
        {
            if (typeof(WorkerFunction).GetProperty(sortby.Substring(1)) == null)
                return new List<WorkerFunction>();
            else if (sortby[0] == '-')
                return workerFunction.OrderByDescending(c => c.GetType()
                                                              .GetProperty(sortby.Substring(1))
                                                              .GetValue(c, null))
                                     .ToList().GetRange(from, limit);
            else
                return workerFunction.OrderBy(c => c.GetType()
                                                    .GetProperty(sortby.Substring(1))
                                                    .GetValue(c, null))
                                     .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<WorkerFunction> Get()
        {
            if (_context.WorkerFunctions != null)
                return _context.WorkerFunctions.ToList();
            else
                return new List<WorkerFunction>();
        }

        [HttpGet("{id}", Name = "GetWorkerFunction")]
        public IActionResult Get(long id)
        {
            var item = _context.WorkerFunctions.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] WorkerFunction item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.WorkerFunctions.Count() + 1;
            _context.WorkerFunctions.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetWorkerFunction", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] WorkerFunction item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var workerFunction = _context.WorkerFunctions.FirstOrDefault(t => t.Id == id);
            if (workerFunction == null)
            {
                return NotFound();
            }

            workerFunction.WorkerId= item.WorkerId;
            workerFunction.FunctionId = item.FunctionId;
            workerFunction.Comment = item.Comment;
            workerFunction.AuthorId = item.AuthorId;
            workerFunction.DateOfRecordCreation = item.DateOfRecordCreation;
            workerFunction.DateOfRecordRemoval = item.DateOfRecordRemoval;

            _context.WorkerFunctions.Update(workerFunction);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var workerFunction = _context.WorkerFunctions.FirstOrDefault(t => t.Id == id);
            if (workerFunction == null)
            {
                return NotFound();
            }

            _context.WorkerFunctions.Remove(workerFunction);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
