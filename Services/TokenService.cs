using favorites.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using favorites.Services.Interfaces;

namespace favorites.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(User user)
        {
            //Instanciando o token
            var tokenHandler = new JwtSecurityTokenHandler();

            //Resgando a chave privada
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            //Criando o token com suas propriedades
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject é as propriedades que irá conter no payload do token
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(24),
                //Assinatura de segurança do token e o tipo de criptografia
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //Gerando o token com as propriedades
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public long GetUserIdFromToken(string token)
        {
            // Decodificar o token para recuperar as informações
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var tokenSimples = handler.ReadToken(token) as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

            // Acessar o ID do usuário do token (supondo que esteja armazenado como um claim)
            var userId = tokenSimples?.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            return long.Parse(userId ?? "-1");
        }
    }
}
