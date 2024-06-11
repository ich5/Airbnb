
using Airbnb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Airbnb.Data;
using Microsoft.AspNetCore.Authorization;

namespace Airbnb.Controllers
{
    [Authorize(Policy = "BasicAuthentication")]
    [ApiController]
    [Route("[controller]")]
    public class AmenitiesController : ControllerBase
    {
        private readonly DataContext _database;

        public AmenitiesController()
        {
            string connectionString = "server=127.0.0.1; port=3306; database=airbnb; user=root; password=;";
            _database = new DataContext(connectionString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Amenities>> Get()
        {
            var amenities = _database.GetAmenities();
            return Ok(amenities);
        }

        [HttpGet("{id}")]
        public ActionResult<Amenities> GetAmenityById(int id)
        {
            var amenity = _database.GetAmenityById(id);
            if (amenity != null)
            {
                return Ok(amenity);
            }
            else
            {
                return NotFound(); // Amenity with the specified ID not found
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Amenities amenity)
        {
            _database.AddAmenity(amenity);
            return Ok("Amenity added successfully");
        }
    }
}
