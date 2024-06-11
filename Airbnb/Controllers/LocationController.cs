
using Airbnb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Airbnb.Data;
using Airbnb.Models.Airbnb.Models;
using Microsoft.AspNetCore.Authorization;

namespace Airbnb.Controllers
{
    [Authorize(Policy = "BasicAuthentication")]
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly DataContext _database;

        public LocationController()
        {
            string connectionString = "server=127.0.0.1; port=3306; database=airbnb; user=root; password=;";
            _database = new DataContext(connectionString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            var locations = _database.GetLocations();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public ActionResult<Location> GetLocationById(int id)
        {
            var location = _database.GetLocationById(id);
            if (location != null)
            {
                return Ok(location);
            }
            else
            {
                return NotFound(); // Location with the specified ID not found
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Location location)
        {
            _database.AddLocation(location);
            return Ok("Location added successfully");
        }
    }
}
