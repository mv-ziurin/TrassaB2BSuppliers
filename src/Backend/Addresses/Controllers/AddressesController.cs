using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Addresses.Database;
using Addresses.Database.Models;
using Microsoft.EntityFrameworkCore;


namespace Addresses.Controllers
{
    [Route("api/[controller]")]
    public class AddressesController : Controller
    {
        private readonly AddressesContext _context;
        private const int _from = 0;
        private const int _limit = 10;
        private const string _sortby = "+Id";

        public AddressesController(AddressesContext context)
        {
            _context = context;
        }

        public List<Address> Sort(DbSet<Address> addresses, int from, int limit, string sortby)
        {
            if (typeof(Address).GetProperty(sortby.Substring(1)) == null)
                return new List<Address>();
            else if (sortby[0] == '-')
                return addresses.OrderByDescending(a => a.GetType()
                                                         .GetProperty(sortby.Substring(1))
                                                         .GetValue(a, null))
                                .ToList().GetRange(from, limit);
            else
                return addresses.OrderBy(a => a.GetType()
                                               .GetProperty(sortby.Substring(1))
                                               .GetValue(a, null))
                                .ToList().GetRange(from, limit);
        }

        [HttpGet]
        public IEnumerable<Address> Get()
        {
            if (_context.Addresses.Count() != 0)
            {
                int from = (Request.Query.Keys.Contains("from")) ? Convert.ToInt32(Request.Query["from"]) : _from;
                int limit = (Request.Query.Keys.Contains("limit")) ? Convert.ToInt32(Request.Query["limit"]) : _limit;
                string sortby = (Request.Query.Keys.Contains("sortby")) ? Request.Query["sortby"].ToString() : _sortby;

                return Sort(_context.Addresses, from, limit, sortby);
            }
            else
                return new List<Address>();
        }

        [HttpGet("{id}", Name = "GetAddress")]
        public IActionResult Get(long id)
        {
            var item = _context.Addresses.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Address item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = _context.Addresses.Count() + 1;
            _context.Addresses.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetAddress", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Address item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var address = _context.Addresses.FirstOrDefault(t => t.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            address.StreetId = item.StreetId;
            address.DistrictId = item.DistrictId;
            address.House = item.House;
            address.HouseBlock = item.HouseBlock;
            address.Building = item.Building;
            address.HomeOwnership = item.HomeOwnership;
            address.Ownership = item.Ownership;
            address.Apartment = item.Apartment;
            address.Postcode = item.Postcode;
            address.Entrance = item.Entrance;
            address.Intercom = item.Intercom;
            address.Floor = item.Floor;
            address.Pavilion = item.Pavilion;
            address.Latitude = item.Latitude;
            address.Longitude = item.Longitude;
            address.Comment = item.Comment;

            _context.Addresses.Update(address);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var address = _context.Addresses.FirstOrDefault(t => t.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }

}
