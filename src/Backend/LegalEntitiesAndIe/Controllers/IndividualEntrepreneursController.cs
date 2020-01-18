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
    public class IndividualEntrepreneursController : Controller
    {
        private readonly LeAndIeContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public IndividualEntrepreneursController(LeAndIeContext context)
        {
            _context = context;
        }

        public List<IndividualEntrepreneur> Sort(DbSet<IndividualEntrepreneur> individualEntrepreneures, int from, int limit, string sortby)
        {
            if (typeof(IndividualEntrepreneur).GetProperty(sortby.Substring(1)) == null)
                return new List<IndividualEntrepreneur>();
            else if (sortby[0] == '-')
                return individualEntrepreneures.OrderByDescending(i => i.GetType()
                                                                        .GetProperty(sortby.Substring(1))
                                                                        .GetValue(i, null))
                                               .ToList().GetRange(from, limit);
            else
                return individualEntrepreneures.OrderBy(i => i.GetType()
                                                              .GetProperty(sortby.Substring(1))
                                                              .GetValue(i, null))
                                               .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<IndividualEntrepreneur> Get()
        {
            if (_context.IndividualEntrepreneurs.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.IndividualEntrepreneurs, from, limit, sortby);
            }
            else
                return new List<IndividualEntrepreneur>();
        }

        [HttpGet("{id}", Name = "GetIndividualEntrepreneur")]
        public IActionResult Get(long id)
        {
            var item = _context.IndividualEntrepreneurs.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IndividualEntrepreneur item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.IndividualEntrepreneurs.Count() + 1;
            _context.IndividualEntrepreneurs.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetIndividualEntrepreneur", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] IndividualEntrepreneur item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var individualEntrepreneur = _context.IndividualEntrepreneurs.FirstOrDefault(t => t.Id == id);
            if (individualEntrepreneur == null)
            {
                return NotFound();
            }


            individualEntrepreneur.EntryDateInOgrnip = item.EntryDateInOgrnip;
            individualEntrepreneur.Ogrnip = item.Ogrnip;
            individualEntrepreneur.TerminationDate = item.TerminationDate;
            individualEntrepreneur.Inn = item.Inn;
            individualEntrepreneur.NaturalPersonId = item.NaturalPersonId;


            _context.IndividualEntrepreneurs.Update(individualEntrepreneur);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var IndividualEntrepreneur = _context.IndividualEntrepreneurs.FirstOrDefault(t => t.Id == id);
            if (IndividualEntrepreneur == null)
            {
                return NotFound();
            }

            _context.IndividualEntrepreneurs.Remove(IndividualEntrepreneur);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}