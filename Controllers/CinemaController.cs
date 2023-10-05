using AutoMapper;
using FilmeApi.Data;
using FilmeApi.Data.Dtos;
using FilmeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaCinema([FromBody] CinemaCreateDto cinemaCreateDto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaCreateDto);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id }, cinemaCreateDto);
    }

    [HttpGet]
    public IEnumerable<CinemaReadDto> RecuperaCinemas([FromBody] int? enderecoId = null)
    {
        if (enderecoId == null)
            return _mapper.Map<List<CinemaReadDto>>(_context.Cinemas.ToList());

        return _mapper.Map<List<CinemaReadDto>>(_context.Cinemas.
            FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM cinemas where cinemas.EnderecoId = {enderecoId}").ToList());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaCinemasPorId(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema != null)
        {
            CinemaReadDto cinemaReadDto = _mapper.Map<CinemaReadDto>(cinema);
            return Ok(cinemaReadDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaCinema(int id, [FromBody] CinemaUpdateDto cinemaUpdateDto)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null)
            return NotFound();

        _mapper.Map(cinemaUpdateDto, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaCinema(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if(cinema == null)
            return NotFound();

        _context.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }
}               