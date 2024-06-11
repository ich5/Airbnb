
using Airbnb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Airbnb.Data;
using Airbnb.Models.Airbnb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Controllers
{
    [Authorize(Policy = "BasicAuthentication")]
    [ApiController]
    [Route("[controller]")]
    public class CampingSpaceController : ControllerBase
    {
        private readonly DataContext _database;

        public CampingSpaceController()
        {
            string connectionString = "server=127.0.0.1; port=3306; database=airbnb; user=root; password=;";
            _database = new DataContext(connectionString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CampingSpace>> Get()
        {
            var campingSpaces = _database.GetCampingSpaces();
            return Ok(campingSpaces);
        }

        [HttpGet("{id}")]
        public ActionResult<CampingSpace> GetCampingSpaceById(int id)
        {
            var campingSpace = _database.GetCampingSpaceById(id);
            if (campingSpace != null)
            {
                return Ok(campingSpace);
            }
            else
            {
                return NotFound(); // CampingSpace with the specified ID not found
            }
        }

        [HttpGet("owner/{ownerId}")]
        public IActionResult GetCampingSpacesByOwner(int ownerId)
        {
            var campingSpaces = _database.GetCampingSpacesByOwnerId(ownerId);
            if (campingSpaces == null || !campingSpaces.Any())
            {
                return NotFound();
            }
            return Ok(campingSpaces);
        }

        [HttpPut("updateAvailability/{campingSpaceId}")]
        public IActionResult UpdateCampingSpaceAvailability(int campingSpaceId, [FromBody] bool availability)
        {
            try
            {
                _database.UpdateCampingSpaceAvailability(campingSpaceId, availability);
                return Ok(new { Message = "Availability updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        // GET: api/CampingSpace/availability/5
        [HttpGet("availability/{id}")]
        public ActionResult<bool> GetCampingSpaceAvailability(int id)
        {
            // Assume a method to check if the camping space exists
            bool spaceExists = _database.CampingSpaceExists(id);

            if (!spaceExists)
            {
                return NotFound();
            }

            bool isAvailable = _database.GetCampingSpaceAvailability(id);

            return Ok(isAvailable);
        }



        [HttpPost]
        public ActionResult Post([FromBody] CampingSpace campingSpace)
        {
            _database.AddCampingSpace(campingSpace);
            return Ok("CampingSpace added successfully");
        }


    }
}
