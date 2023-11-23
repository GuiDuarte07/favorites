using favorites.Models;
using favorites.Models.DTOs.User;
using favorites.Models.Entities;
using favorites.Repositories.Interfaces;
using favorites.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace favorites.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserDO userDTO)
        {
            // Não sei se essa verificação é necessária
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userInfoDTO = await _userRepository.CreateUserAsync(userDTO);

            return CreatedAtAction(nameof(GetUser), new { id = userInfoDTO.Id }, userInfoDTO);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoDTO>> GetUser(long id)
        {
            
            var userInfo = await _userRepository.GetUserByIdAsync(id);

            if (userInfo == null)
            {
                return NotFound();
            }

            return userInfo;
        }
    }
}
