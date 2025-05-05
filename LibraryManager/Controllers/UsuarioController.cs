using AutoMapper;
using LibraryManager.Data;
using LibraryManager.Data.Dtos;
using LibraryManager.Models;
using LibraryManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LibraryManager.Controllers;


[ApiController]
[Route("[Controller]")]
public class UsuarioController : Controller
{

    private UsuarioService _usuarioService;
    private UsuarioDbContext _dbContext;
    public UsuarioController(UsuarioService cadastroService, UsuarioDbContext dbContext)
    {
        _usuarioService = cadastroService;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserByCPF (string CPF)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.CPF == CPF);
        if (user == null) return NotFound();
        await _usuarioService.GetUser(user);
        return Ok(user);
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        var verificaCPFExistente = _dbContext.Users.FirstOrDefault(a=>a.CPF == dto.CPF);
        if (verificaCPFExistente != null)
        return BadRequest("CPF já cadastrado");
        await _usuarioService.Cadastra(dto);
        return Ok("Usuário cadastrado com sucesso");
    }
    
    [HttpPost("login")] 
    public async Task<IActionResult> LoginAsync(LoginUsuarioDto dto)
    {
        var token = await _usuarioService.LoginAsync(dto);
        return Ok(token);
    }

    [HttpPut]
    public IActionResult UpdateUser([FromQuery] string CPF, [FromBody] UpdateUsuarioDto dto)
    {
            var user = _dbContext.Users.Where(u => u.CPF == CPF).FirstOrDefault();
            if (user == null) return NotFound();
            user.UserName = dto.UserName;
            user.DataNascimento = dto.DataNascimento;
            user.NormalizedUserName = dto.UserName.ToUpper();
            _dbContext.SaveChanges();
            return Ok(dto);
    }

    [HttpDelete("Delete")]
    public IActionResult DeleteUser(string CPF)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.CPF == CPF);
        if (user == null) return NotFound();
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
        return NoContent();
    }
   
}
