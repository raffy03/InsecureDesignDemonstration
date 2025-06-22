using Microsoft.AspNetCore.Mvc;
using InsecureDesignDemo.Models;
using System.Linq;

namespace InsecureDesignDemo.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("promote-user/{id}")]
        public IActionResult PromoteUser(int id)
        {
            // no check if user is admin 
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.Role = "Admin";
                _context.SaveChanges();
            }
            return Ok("User promoted!");
        }
    }
}