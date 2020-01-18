using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LegalEntitiesAndIe.Database;
using LegalEntitiesAndIe.Database.Models;

namespace LegalEntitiesAndIe.Controllers
{
    [Route("api/[controller]")]
    public class LegalEntitiesController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public LegalEntitiesController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<LegalEntity> Sort(DbSet<LegalEntity> legalEntity, int from, int limit, string sortby)
        {
            if (typeof(LegalEntity).GetProperty(sortby.Substring(1)) == null)
                return new List<LegalEntity>();
            else if (sortby[0] == '-')
                return legalEntity.OrderByDescending(i => i.GetType()
                                                           .GetProperty(sortby.Substring(1))
                                                           .GetValue(i, null))
                                  .ToList().GetRange(from, limit);
            else
                return legalEntity.OrderBy(i => i.GetType()
                                                 .GetProperty(sortby.Substring(1))
                                                 .GetValue(i, null))
                                  .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<LegalEntity> Get()
        {
            if (_context.LegalEntities.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.LegalEntities, from, limit, sortby);
            }
            else
                return new List<LegalEntity>();
        }

        [HttpGet("{id}", Name = "GetLegalEntity")]
        public IActionResult Get(long id)
        {
            var item = _context.LegalEntities.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] LegalEntity item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.LegalEntities.Count() + 1;
            _context.LegalEntities.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetLegalEntity", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] LegalEntity item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var legalEntity = _context.LegalEntities.FirstOrDefault(t => t.Id == id);
            if (legalEntity == null)
            {
                return NotFound();
            }

            legalEntity.AssignmentDateOfOgrn = item.AssignmentDateOfOgrn;
            legalEntity.CentralOfficeAddressId = item.CentralOfficeAddressId;
            legalEntity.Founder = item.Founder;
            legalEntity.Head = item.Head;
            legalEntity.PostAddressId = item.PostAddressId;
            legalEntity.TerminationDate = item.TerminationDate;
            legalEntity.LegalAddressId = item.LegalAddressId;

            _context.LegalEntities.Update(legalEntity);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var legalEntity = _context.LegalEntities.FirstOrDefault(t => t.Id == id);
            if (legalEntity == null)
            {
                return NotFound();
            }

            _context.LegalEntities.Remove(legalEntity);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}