using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LegalWorkers.Database;
using LegalWorkers.Database.Models;
using System;

namespace LegalWorkers.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesFunctionController : Controller
    {
        private readonly LegalWorkersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public EmployeesFunctionController(LegalWorkersContext context)
        {
            _context = context;
        }

        public List<EmployeeFunction> Sort(DbSet<EmployeeFunction> employeeFunction, int from, int limit, string sortby)
        {
            if (typeof(EmployeeFunction).GetProperty(sortby.Substring(1)) == null)
                return new List<EmployeeFunction>();
            else if (sortby[0] == '-')
                return employeeFunction.OrderByDescending(c => c.GetType()
                                                                .GetProperty(sortby.Substring(1))
                                                                .GetValue(c, null))
                                       .ToList().GetRange(from, limit);
            else
                return employeeFunction.OrderBy(c => c.GetType()
                                                      .GetProperty(sortby.Substring(1))
                                                      .GetValue(c, null))
                                       .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<EmployeeFunction> Get()
        {
            if (_context.EmployeeFunctions.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.EmployeeFunctions, from, limit, sortby);
            }
            else
                return new List<EmployeeFunction>();
        }

        [HttpGet("{id}", Name = "GetEmployeeFunction")]
        public IActionResult Get(long id)
        {
            var item = _context.EmployeeFunctions.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmployeeFunction item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.EmployeeFunctions.Count() + 1;
            _context.EmployeeFunctions.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetEmployeeFunction", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] EmployeeFunction item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var employeeFunction = _context.EmployeeFunctions.FirstOrDefault(t => t.Id == id);
            if (employeeFunction == null)
            {
                return NotFound();
            }

            employeeFunction.CounteragentType= item.CounteragentType;
            employeeFunction.Name = item.Name;
            employeeFunction.Description = item.Description;

            _context.EmployeeFunctions.Update(employeeFunction);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var employeeFunction = _context.EmployeeFunctions.FirstOrDefault(t => t.Id == id);
            if (employeeFunction == null)
            {
                return NotFound();
            }

            _context.EmployeeFunctions.Remove(employeeFunction);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
