using System.ComponentModel.DataAnnotations;

namespace Konsi.Api.Dtos;

public class MatriculaRequest
{
    [Required]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido")]
    public string Cpf { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "O nome de usuário não pode ter mais de 50 caracteres")]
    public string Username { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "A senha não pode ter mais de 100 caracteres")]
    public string Password { get; set; }
}
