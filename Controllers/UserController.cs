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
        private readonly IHashService _hashService;

        public UserController(FavoriteContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
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
            string hashedPassword = _hashService.HashPassword(userDTO.Password);

            // Instânciando uma nova classe User com os dados do userDTO
            var user = new User { Name = userDTO.Name, Email = userDTO.Email, Password = hashedPassword };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new UserInfoDTO { Email = user.Email, Name = user.Name, Id = user.Id });
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
