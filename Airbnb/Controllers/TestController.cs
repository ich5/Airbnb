using Microsoft.AspNetCore.Mvc;
using Airbnb.Data;
using System.Linq;

namespace Airbnb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Try to fetch some data from the database
            var users = _context.Users.ToList();
            return Ok(users);
        }
    }
}
