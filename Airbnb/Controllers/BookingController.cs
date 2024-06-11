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
    public class BookingController : ControllerBase
    {
        private readonly DataContext _database;

        public BookingController()
        {
            string connectionString = "server=127.0.0.1; port=3306; database=airbnb; user=root; password=;";
            _database = new DataContext(connectionString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Booking>> Get()
        {
            var bookings = _database.GetBookings();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public ActionResult<Booking> GetBookingById(string id)
        {
            var booking = _database.GetBookingById(id);
            if (booking != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound(); // Booking with the specified ID not found
            }
        }

        [HttpGet("GetBookingsByUser/{userId}")]
        public IActionResult GetBookingsByUser(int userId)
        {
            var bookings = _database.GetBookingsByUserId(userId);
            if (bookings == null)
            {
                return NotFound();
            }
            return Ok(bookings);
        }

        [HttpGet("GetBookingsByCampingSpace/{campingSpaceId}")]
        public IActionResult GetBookingsByCampingSpace(int campingSpaceId)
        {
            var bookings = _database.GetBookingsByCampingSpaceId(campingSpaceId);
            if (bookings == null || !bookings.Any())
            {
                return NotFound();
            }
            return Ok(bookings);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Booking booking)
        {
            _database.AddBooking(booking);
            return Ok("Booking added successfully");
        }
    }
}
