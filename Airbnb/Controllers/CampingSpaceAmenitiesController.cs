
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
    public class CampingSpaceAmenitiesController : ControllerBase
    {
        private readonly DataContext _database;

        public CampingSpaceAmenitiesController()
        {
            string connectionString = "server=127.0.0.1; port=3306; database=airbnb; user=root; password=;";
            _database = new DataContext(connectionString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CampingSpaceAmenities>> Get()
        {
            var campingSpaceAmenities = _database.GetCampingSpaceAmenities();
            return Ok(campingSpaceAmenities);
        }

        [HttpGet("{id}")]
        public ActionResult<CampingSpaceAmenities> GetCampingSpaceAmenityById(int id)
        {
            var campingSpaceAmenity = _database.GetCampingSpaceAmenityById(id);
            if (campingSpaceAmenity != null)
            {
                return Ok(campingSpaceAmenity);
            }
            else
            {
                return NotFound(); // CampingSpaceAmenity with the specified ID not found
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] CampingSpaceAmenities campingSpaceAmenity)
        {
            _database.AddCampingSpaceAmenity(campingSpaceAmenity);
            return Ok("CampingSpaceAmenity added successfully");
        }
    }
}
