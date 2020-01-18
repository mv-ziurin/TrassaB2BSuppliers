using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Database;
using Suppliers.Database.Models;
using Microsoft.EntityFrameworkCore;


namespace Suppliers.Controllers
{
    [Route("api/[controller]")]
    public class NetPromoterScoresController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public NetPromoterScoresController(SuppliersContext context)
        {
            _context = context;
        }

        public List<NetPromoterScore> Sort(DbSet<NetPromoterScore> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<NetPromoterScore>();
            else if (sortby[0] == '-')
                return Brandes.OrderByDescending(a => a.GetType()
                                                         .GetProperty(sortby.Substring(1))
                                                         .GetValue(a, null))
                                .ToList().GetRange(from, limit);
            else
                return Brandes.OrderBy(a => a.GetType()
                                               .GetProperty(sortby.Substring(1))
                                               .GetValue(a, null))
                                .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<NetPromoterScore> Get()
        {
            if (_context.NetPromoterScores.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.NetPromoterScores, from, limit, sortby);
            }
            else
                return new List<NetPromoterScore>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.NetPromoterScores.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NetPromoterScore item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.NetPromoterScores.Count() + 1;
            _context.NetPromoterScores.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] NetPromoterScore item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var record = _context.NetPromoterScores.FirstOrDefault(t => t.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            record.BrandId = item.BrandId;
            record.ConsumerProfileId = item.ConsumerProfileId;
            record.Respondent = item.Respondent;
            record.Year = item.Year;
            record.Month = item.Month;
            record.RecomendationQuestion = item.RecomendationQuestion;
            record.NumberOfPositiveResponds = item.NumberOfPositiveResponds;
            record.NumberOfNegativeResponds = item.NumberOfNegativeResponds;
            record.NumberOfNeutralResponds = item.NumberOfNeutralResponds;

            _context.NetPromoterScores.Update(record);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var record = _context.NetPromoterScores.FirstOrDefault(t => t.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            _context.NetPromoterScores.Remove(record);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}