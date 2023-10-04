using System.ComponentModel.DataAnnotations;

namespace FilmeApi.Data.Dtos;

public class CinemaCreateDto
{
    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    public string Nome { get; set; }
    public int EnderecoId { get; set; }
}
