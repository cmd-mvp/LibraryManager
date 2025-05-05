using AutoMapper;
using LibraryManager.Data;
using LibraryManager.Data.Dtos;
using LibraryManager.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Controllers;

[ApiController]
[Route("[Controller]")]
public class LivroController : ControllerBase
{
    private LivroContext _context;
    private IMapper _mapper;
    public LivroController(LivroContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// <param name="livroDto">Objeto com os campos necessários para criação de um livro</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult PostLivro([FromBody] CreateLivroDto livroDto)
    {
        Livro livro = _mapper.Map<Livro>(livroDto);
        _context.Livros.Add(livro);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetLivroId), new { id = livro.Id, titulo = livro.Titulo }, livro);
    }

    [HttpGet]
    public IEnumerable<ReadLivroDto> GetLivro([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
            return _mapper.Map<List<ReadLivroDto>>(_context.Livros.Skip(skip).Take(take).ToList()); 
    }

    /* [HttpGet("autor/{NomeAutor}")]
     public IActionResult GetLivrosDoAutor(string NomeAutor)
     {
         var livro = _context.Livros.FirstOrDefault(livro => livro.Autor == NomeAutor);
         if (NomeAutor == null) return NotFound();
         var dto = _mapper.Map<ReadLivroDto>(livro);
         return Ok(livro);

     }*/

    /* [HttpGet("autor")]
     public IEnumerable<Livro> GetLivrosDoAutor([FromQuery] string? NomeAutor)
     {
         var check = _context.Livros.FirstOrDefault(a=>a.Autor.Equals(NomeAutor));
         if (check != null)
         {
         return _mapper.Map<List<Livro>>(_context.Livros.FromSqlRaw($"select * from livros where autor = \"{NomeAutor}\"; \r\n").ToList());
         }
         return _mapper.Map<List<Livro>>(_context.Livros.ToList());
     }*/

    [HttpGet("autor")]
    public IActionResult GetLivrosDoAutor([FromQuery] string? NomeAutor)
    {
        var check = _context.Livros.FirstOrDefault(a => a.Autor.Equals(NomeAutor));
        if (check == null) return NotFound();
        var VAICORINTHIANS = _context.Livros.FromSqlRaw($"select * from livros where autor = \"{NomeAutor}\"; \r\n").ToList();
        return Ok(VAICORINTHIANS); 
        
    }

    [HttpGet("{id}")]
    public IActionResult GetLivroId(int id)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null) return NotFound();
        var dto = _mapper.Map<ReadLivroDto>(livro);
        return Ok(livro);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaLivro(int id, [FromBody] UpdateLivroDto livroDto)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null) return NotFound();
        _mapper.Map(livroDto, livro);
        _context.SaveChanges();
        return NoContent();
    }


    [HttpPatch("{id}")]
    public IActionResult PatchLivro(int id, JsonPatchDocument<UpdateLivroDto> jsonPatch)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null) return NotFound();

        var livroparaAtualizar = _mapper.Map<UpdateLivroDto>(livro);

        //Se não conseguir validar o modelo de livrosparaAtualizar, retornar um ValidationProblem(ModelState).
        jsonPatch.ApplyTo(livroparaAtualizar, ModelState);

        if (!TryValidateModel(livroparaAtualizar))

        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(livroparaAtualizar, livro);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteLivro(int id)
    {
        var livro = _context.Livros.FirstOrDefault(livro =>livro.Id == id);
        if (livro == null) return NotFound();
        _context.Remove(livro);
        _context.SaveChanges();
        return NoContent();
    }
    
}
