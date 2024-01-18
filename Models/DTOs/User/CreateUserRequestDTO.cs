using System.ComponentModel.DataAnnotations;

namespace favorites.Models.DTOs.User
{
    public class CreateUserRequestDTO
    {
        [Required(ErrorMessage = "O campo 'Name' é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo 'Email' deve ser um endereço de e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo 'Password' é obrigatório.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Password { get; set; }
    }
}
