using System.ComponentModel.DataAnnotations;

namespace FilmeApi.Data.Dtos;

public class CinemaUpdateDto
{
    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    public string Nome { get; set; }
}
