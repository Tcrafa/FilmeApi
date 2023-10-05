using AutoMapper;
using FilmeApi.Data;
using FilmeApi.Data.Dtos;
using FilmeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmeApi.Controllers;

[ApiController]
[Route("controller")]
public class SessaoController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public SessaoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaSessao(SessaoCreateDto sessaoDto)
    {
        Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaSessoesPorId), new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId }, sessao);
    }

    [HttpGet]
    public IEnumerable<SessaoReadDto> RecuperaSessoes()
    {
        return _mapper.Map<List<SessaoReadDto>>(_context.Sessoes.ToList());
    }

    [HttpGet("{filmeId}/{cinemaId}")]
    public IActionResult RecuperaSessoesPorId(int filmeId, int cinemaId)
    {
        Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
        if(sessao != null)
        {
            SessaoReadDto sessaoDto = _mapper.Map<SessaoReadDto>(sessao);

            return Ok(sessaoDto);
        }
        return NotFound();
    }
}
