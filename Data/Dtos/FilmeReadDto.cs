namespace FilmeApi.Data.Dtos;

public class FilmeReadDto
{
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracao { get; set; }
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    public ICollection<SessaoReadDto> Sessoes { get; set; }
}
