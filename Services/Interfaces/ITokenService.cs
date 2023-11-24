using favorites.Models.DTOs.User;
using favorites.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace favorites.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);

        public long GetUserIdFromToken(string token);
    }
}
