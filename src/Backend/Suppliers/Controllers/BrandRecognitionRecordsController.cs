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
    public class BrandRecognitionRecordsController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public BrandRecognitionRecordsController(SuppliersContext context)
        {
            _context = context;
        }

        public List<BrandRecognition> Sort(DbSet<BrandRecognition> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<BrandRecognition>();
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
        public IEnumerable<BrandRecognition> Get()
        {
            if (_context.BrandRecognitionRecords.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.BrandRecognitionRecords, from, limit, sortby);
            }
            else
                return new List<BrandRecognition>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.BrandRecognitionRecords.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BrandRecognition item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.BrandRecognitionRecords.Count() + 1;
            _context.BrandRecognitionRecords.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] BrandRecognition item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var record = _context.BrandRecognitionRecords.FirstOrDefault(t => t.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            record.BrandId = item.BrandId;
            record.ConsumerProfileId = item.ConsumerProfileId;
            record.Respondent = item.Respondent;
            record.Year = item.Year;
            record.Month = item.Month;
            record.QualityQuestion = item.QualityQuestion;
            record.NumberOfPositiveResponds = item.NumberOfPositiveResponds;
            record.NumberOfNegativeResponds = item.NumberOfNegativeResponds;
            record.NumberOfNeutralResponds = item.NumberOfNeutralResponds;

            _context.BrandRecognitionRecords.Update(record);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var record = _context.BrandRecognitionRecords.FirstOrDefault(t => t.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            _context.BrandRecognitionRecords.Remove(record);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}



