using InsecureDesignDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Roles = "Admin")]
        [HttpPost("promote-user/{id}")]
        public IActionResult PromoteUser(int id)
        {
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