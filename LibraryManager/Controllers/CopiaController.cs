using AutoMapper;
using LibraryManager.Data;
using LibraryManager.Data.Dtos;
using LibraryManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Controllers;

[ApiController]
[Route("[Controller]")]
public class CopiaController : ControllerBase
{
    private LivroContext _context;
    private IMapper _mapper;

    public CopiaController(LivroContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult PostCopia([FromBody]CreateCopiaDto dto)
    {
        Copia copia = _mapper.Map<Copia>(dto);
        _context.Copias.Add(copia);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCopiaById), new {Id = copia.Id}, copia);
    }

    [HttpGet("disponivel")]
    public IEnumerable<ReadCopiaDto> GetCopiasECopiasDisponiveis([FromQuery] int? livroId = null)
    {
        var livro = _context.Copias.FirstOrDefault(l=>l.LivroId == livroId);
        if (livro != null)
        {
            return _mapper.Map<List<ReadCopiaDto>>(_context.Copias.FromSqlRaw($"SELECT Id, livroId, Disponivel FROM copias Where copias.Disponivel = true and livroId = {livroId}").ToList());
        }

        return _mapper.Map<List<ReadCopiaDto>>(_context.Copias.ToList());
    }

    [HttpGet("{Id}")]
    public IActionResult GetCopiaById(int Id)
    {
        Copia copia = _context.Copias.FirstOrDefault(copia => copia.Id == Id);
        if (copia != null)
        {
            ReadCopiaDto copiaDto = _mapper.Map<ReadCopiaDto>(copia);

            return Ok(copiaDto);
        }
        return NotFound();
    }
   
    /* [HttpGet("{disponivel}/{livroId}")]

    //$"SELECT Id, livroId, Disponivel FROM copias Where copias.Disponivel = 1 and livroId = {livroId}"
    public IActionResult GetCopiaId([FromQuery] bool disponivel, int livroId)
    {
        var livro = _context.Copias.FirstOrDefault(c=>c.LivroId == livroId);
        if (livro == null)
        {
            return NotFound();
        }
        var filtro = _mapper.Map<Copia>(_context.Copias.FirstOrDefault(d=>d.Disponivel == disponivel));
        if (filtro == null) return NotFound();
        var resultado = _context.Copias.FromSqlInterpolated($"SELECT Id, livroId, Disponivel FROM copias Where copias.Disponivel = {filtro} and livroId = {livroId}").ToList();
        return Ok(resultado);
    }*/

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var copia = _context.Copias.FirstOrDefault(c=>c.Id == id);
        if (copia == null) return NotFound();
        _context.Copias.Remove(copia);
        _context.SaveChanges();
        return NoContent();
    }
    

}
