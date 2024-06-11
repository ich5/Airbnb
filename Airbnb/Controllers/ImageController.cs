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
    public class ImageController : ControllerBase
    {
        private readonly DataContext _database;

        public ImageController()
        {
            string connectionString = "server=127.0.0.1; port=3306; database=airbnb; user=root; password=;";
            _database = new DataContext(connectionString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Image>> Get()
        {
            var images = _database.GetImages();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public ActionResult<Image> GetImageById(int id)
        {
            var image = _database.GetImageById(id);
            if (image != null)
            {
                return Ok(image);
            }
            else
            {
                return NotFound(); // Image with the specified ID not found
            }
        }

        [HttpGet("GetImagesByCampingSpace/{campingSpaceId}")]
        public ActionResult<IEnumerable<Image>> GetImagesByCampingSpace(int campingSpaceId)
        {
            var images = _database.GetImagesByCampingSpace(campingSpaceId);
            return Ok(images);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Image image)
        {
            _database.AddImage(image);
            return Ok("Image added successfully");
        }
    }
}
