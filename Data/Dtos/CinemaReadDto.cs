namespace FilmeApi.Data.Dtos;

public class CinemaReadDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public EnderecoReadDto Endereco { get; set; }
    public ICollection<SessaoReadDto> Sessoes { get; set; }
}
