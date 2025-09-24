using AutoMapper;
using LibraryManager.Data;
using LibraryManager.Data.Dtos;
using LibraryManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LibraryManager.Controllers;

[ApiController]
[Route("[Controller]")]

public class AluguelController : ControllerBase
{
    private LivroContext _context;
    private UsuarioDbContext _userContext;
    private IMapper _mapper;

    public AluguelController(LivroContext context, IMapper mapper, UsuarioDbContext usuario)
    {
        _context = context;
        _mapper = mapper;
        _userContext = usuario;
    }

    [HttpPost("Alugar")]
    public IActionResult Alugar([FromQuery] CreateAluguelDto dto)
    {
            var user = _userContext.Usuarios.FirstOrDefault(a => a.UserName == dto.UserName);
            if (user == null) return NotFound();
            var copia = _context.Copias.FirstOrDefault(a=>a.Id == dto.CopiaId);
           if (copia == null) return NotFound();
            var aluguel = _mapper.Map<Aluguel>(dto);
            aluguel.VerificaDisponivel(copia);
            aluguel.Alugar(copia);
            _context.Add(aluguel);
            _context.SaveChanges();
            return Ok(aluguel);
    }

    [HttpGet("Ver alugueis por STATUS")]
    public IActionResult GetAluguelByStatus(string status)
    {
        var lista = _mapper.Map<List<Aluguel>>(_context.Alugueis.ToList());

        foreach (var item in lista)
        {
            int result = item.Devolucao.CompareTo(DateTime.Now);


            if (result < 0)
            {
                item.StatusDoAluguel = StatusAluguel.Atrasado.ToString();
                _context.SaveChanges();

            }
            else if (result >= 0)
            {
                item.StatusDoAluguel = StatusAluguel.Pendente.ToString();
                _context.SaveChanges();

            }
        }
        var listaVerificada = _context.Alugueis.Where(l => l.StatusDoAluguel.Equals(status)).ToList();
        if (listaVerificada.Count == 0) return NotFound($"Não existem alugueis com o status: {status}");
        return Ok(listaVerificada);
    }

    [HttpGet()]
    public IActionResult GetById([FromQuery] int id)
    {
        var rent = _context.Alugueis.FirstOrDefault(a=>a.Id == id);
        if (rent == null) return NotFound();
        return Ok(rent);
    }

    [HttpGet("Ver todos os alugueis")]
    public IActionResult GetAlugueis()
    {
        var lista = _mapper.Map<List<Aluguel>>(_context.Alugueis.ToList());
        foreach (var item in lista)
        {
            int result = item.Devolucao.CompareTo(DateTime.Now);


            if (result < 0)
            {
                item.StatusDoAluguel = StatusAluguel.Atrasado.ToString();
                _context.SaveChanges();

            }
            else if (result >= 0)
            {
                item.StatusDoAluguel = StatusAluguel.Pendente.ToString();
                _context.SaveChanges();

            }
        }
        return Ok(lista);
    }

    [HttpDelete("Devolver Cópia")]
    public IActionResult Devolver([FromQuery]int id)
    {
        var aluguel = _context.Alugueis.FirstOrDefault(a => a.Id == id);
        if (aluguel == null) return NotFound();
        var copia = _context.Copias.FirstOrDefault(a=>a.Id == aluguel.CopiaId);
        aluguel.Devolver(copia!);
        _context.Remove(aluguel);
        _context.SaveChanges();
        return NoContent();
    }

}
