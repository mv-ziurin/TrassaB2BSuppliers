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
    public class SeasonsController : Controller
    {
        private readonly SuppliersContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public SeasonsController(SuppliersContext context)
        {
            _context = context;
        }

        public List<Season> Sort(DbSet<Season> Brandes, int from, int limit, string sortby)
        {
            if (typeof(Brand).GetProperty(sortby.Substring(1)) == null)
                return new List<Season>();
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
        public IEnumerable<Season> Get()
        {
            if (_context.Seasons.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Seasons, from, limit, sortby);
            }
            else
                return new List<Season>();
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult Get(long id)
        {
            var item = _context.Seasons.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Season item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Seasons.Count() + 1;
            _context.Seasons.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBrand", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Season item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var season = _context.Seasons.FirstOrDefault(t => t.Id == id);
            if (season == null)
            {
                return NotFound();
            }

            season.SeasonType = item.SeasonType;
            season.Year = item.Year;
            season.BrandId = item.BrandId;
            season.DateOfCatalogAcquisition = item.DateOfCatalogAcquisition;
            season.DateOfProducerSampleDispatch = item.DateOfProducerSampleDispatch;
            season.DateOfProducerSampleReceipt = item.DateOfProducerSampleReceipt;
            season.DateOfRegionalSampleDispatch = item.DateOfRegionalSampleDispatch;
            season.DateOfRegionalSampleReceipt = item.DateOfRegionalSampleReceipt;
            season.DateOfMoscowShowroomPrep = item.DateOfMoscowShowroomPrep;
            season.DateOfPresentationPrep = season.DateOfPresentationPrep;
            season.DateOfPrepForPartnerBuyersTrainingActivities = season.DateOfPrepForPartnerBuyersTrainingActivities;
            season.DateOfShowroomPhoneInvitations = season.DateOfShowroomPhoneInvitations;
            season.DateOfProducerPhotoAcquisiton = season.DateOfProducerPhotoAcquisiton;
            season.DateOfB2BCatalogFilesAcquisiton = season.DateOfB2BCatalogFilesAcquisiton;
            season.DateOfCollectionShootingEnd = season.DateOfCollectionShootingEnd;
            season.DateOfB2BCollectionAndPricePublishing = season.DateOfB2BCollectionAndPricePublishing;
            season.DateOfB2BPreorderCampaignInvitations = season.DateOfB2BPreorderCampaignInvitations;
            season.DateOfBrandStatisticsAcquisition = season.DateOfBrandStatisticsAcquisition;

            _context.Seasons.Update(season);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var season = _context.Seasons.FirstOrDefault(t => t.Id == id);
            if (season == null)
            {
                return NotFound();
            }

            _context.Seasons.Remove(season);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}


