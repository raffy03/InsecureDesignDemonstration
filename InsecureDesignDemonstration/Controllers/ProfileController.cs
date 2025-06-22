using Microsoft.AspNetCore.Mvc;
using InsecureDesignDemo.Models;
using System.Linq;

namespace InsecureDesignDemo.Controllers
{
    [Route("profile")]
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ViewProfile(int id)
        {
            // no check if user is only viewing his own profile
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}