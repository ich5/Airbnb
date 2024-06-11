
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
    public class ReviewsController : ControllerBase
    {
        private readonly DataContext _database;

        public ReviewsController()
        {
            string connectionString = "server=127.0.0.1; port=3306; database=airbnb; user=root; password=;";
            _database = new DataContext(connectionString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Reviews>> Get()
        {
            var reviews = _database.GetReviews();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public ActionResult<Reviews> GetReviewById(int id)
        {
            var review = _database.GetReviewById(id);
            if (review != null)
            {
                return Ok(review);
            }
            else
            {
                return NotFound(); // Review with the specified ID not found
            }
        }
        // New endpoint to get reviews by camping space ID
        [HttpGet("campingSpace/{campingSpaceId}")]
        public ActionResult<IEnumerable<Reviews>> GetReviewsByCampingSpaceId(int campingSpaceId)
        {
            var reviews = _database.GetReviewsByCampingSpaceId(campingSpaceId);
            return Ok(reviews);
        }


        [HttpPost]
        public ActionResult Post([FromBody] Reviews review)
        {
            _database.AddReview(review);
            return Ok("Review added successfully");
        }
    }
}
