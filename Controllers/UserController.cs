using favorites.Models;
using Microsoft.AspNetCore.Mvc;

namespace favorites.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FavoriteContext _context;

        public UserController(FavoriteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok();
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
