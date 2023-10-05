using AutoMapper;
using FilmeApi.Data;
using FilmeApi.Data.Dtos;
using FilmeApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmeApi.Controllers;

[ApiController]
[Route("controller")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme(
        [FromBody] FilmeCreateDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId),
            new { id = filme.Id },
            filme);
    }

    [HttpGet]
    public IEnumerable<FilmeReadDto> RecuperaFilmes([FromQuery] int skip = 0,
        [FromQuery] int take = 50,
        [FromQuery] string? nomeCinema = null)
    {
        if (nomeCinema == null)
        {
            return _mapper.Map<List<FilmeReadDto>>(_context.Filmes.Skip(skip).Take(take).ToList());
        }
        return _mapper.Map<List<FilmeReadDto>>(_context.Filmes.Skip(skip).Take(take).
            Where(filme => filme.Sessoes.Any(sessao => sessao.Cinema.Nome == nomeCinema)).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes
            .FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<FilmeReadDto>(filme);
        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id,
        [FromBody] FilmeUpdateDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id,
        JsonPatchDocument<FilmeUpdateDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<FilmeUpdateDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
