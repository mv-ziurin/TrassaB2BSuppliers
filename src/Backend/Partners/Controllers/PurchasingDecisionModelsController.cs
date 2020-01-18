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
    public class PurchasingDecisionModelsController : Controller
    {
        private readonly PartnersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public PurchasingDecisionModelsController(PartnersContext context)
        {
            _context = context;
        }

        public List<PurchasingDecisionModel> Sort(DbSet<PurchasingDecisionModel> purchasingDecisionModels, int from, int limit, string sortby)
        {
            if (typeof(PurchasingDecisionModel).GetProperty(sortby.Substring(1)) == null)
                return new List<PurchasingDecisionModel>();
            else if (sortby[0] == '-')
                return purchasingDecisionModels.OrderByDescending(a => a.GetType()
                                                                        .GetProperty(sortby.Substring(1))
                                                                        .GetValue(a, null))
                                               .ToList().GetRange(from, limit);
            else
                return purchasingDecisionModels.OrderBy(a => a.GetType()
                                                              .GetProperty(sortby.Substring(1))
                                                              .GetValue(a, null))
                                               .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<PurchasingDecisionModel> Get()
        {
            if (_context.PurchasingDecisionModels.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.PurchasingDecisionModels, from, limit, sortby);
            }
            else
                return new List<PurchasingDecisionModel>();
        }

        [HttpGet("{id}", Name = "GetPurchasingDecisionModel")]
        public IActionResult Get(long id)
        {
            var item = _context.PurchasingDecisionModels.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PurchasingDecisionModel item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.PurchasingDecisionModels.Count() + 1;
            _context.PurchasingDecisionModels.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetPurchasingDecisionModel", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] PurchasingDecisionModel item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var purchasingDecisionModel = _context.PurchasingDecisionModels.FirstOrDefault(t => t.Id == id);
            if (purchasingDecisionModel == null)
            {
                return NotFound();
            }

            purchasingDecisionModel.NaturalPersonId = item.NaturalPersonId;
            purchasingDecisionModel.Type = item.Type;

            _context.PurchasingDecisionModels.Update(purchasingDecisionModel);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var purchasingDecisionModel = _context.PurchasingDecisionModels.FirstOrDefault(t => t.Id == id);
            if (purchasingDecisionModel == null)
            {
                return NotFound();
            }

            _context.PurchasingDecisionModels.Remove(purchasingDecisionModel);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}