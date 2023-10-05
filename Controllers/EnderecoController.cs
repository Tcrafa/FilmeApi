using AutoMapper;
using FilmeApi.Data;
using FilmeApi.Data.Dtos;
using FilmeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public EnderecoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaEndereco([FromBody] EnderecoCreateDto enderecoDto)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaEnderecosPorId), new { Id = endereco.Id }, endereco);
    }

    [HttpGet]
    public IEnumerable<EnderecoReadDto> RecuperaEnderecos()
    {
        return _mapper.Map<List<EnderecoReadDto>>(_context.Enderecos);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaEnderecosPorId(int id)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco != null)
        {
            EnderecoReadDto enderecoDto = _mapper.Map<EnderecoReadDto>(endereco);

            return Ok(enderecoDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaEndereco(int id, [FromBody] EnderecoUpdateDto enderecoDto)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco == null)
        {
            return NotFound();
        }
        _mapper.Map(enderecoDto, endereco);
        _context.SaveChanges();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult DeletaEndereco(int id)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco == null)
        {
            return NotFound();
        }
        _context.Remove(endereco);
        _context.SaveChanges();
        return NoContent();
    }
}
