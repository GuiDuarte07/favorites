using favorites.Models;
using favorites.Models.DTOs.User;
using favorites.Models.Entities;
using favorites.Services;
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

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserDO userDTO)
        {
            // Não sei se essa verificação é necessária
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Fazendo o hash da senha
            string hashedPassword = HashService.HashPassword(userDTO.Password);

            // Instânciando uma nova classe User com os dados do userDTO
            var user = new User { Name = userDTO.Name, Email = userDTO.Email, Password = hashedPassword };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoDTO>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userInfo = new UserInfoDTO() { Email = user.Email, Folders = user.Folders, Id = user.Id, Name = user.Name };

            return userInfo;
        }
    }
}
