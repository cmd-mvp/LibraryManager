using AutoMapper;
using LibraryManager.Data;
using LibraryManager.Data.Dtos;
using LibraryManager.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryManager.Services;

public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;
    private readonly IPasswordValidator<IdentityUser> _passwordValidator;
    private TokenService _tokenService;
    private readonly UsuarioDbContext _userContext;

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService, UsuarioDbContext userContext)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _userContext = userContext;
    }

    public async Task Cadastra(CreateUsuarioDto dto)
    {
        Usuario usuario = _mapper.Map<Usuario>(dto);

        int allowedYear = 1900;
        int dtoDate = dto.DataNascimento.Year;

        if ( dtoDate < allowedYear)
        {
            throw new ApplicationException($"O ano deve ser maior que {allowedYear}, mas é {dtoDate}");
        }
        IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar usuário");
        }

    }

        public async Task<string> LoginAsync(LoginUsuarioDto dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }

        public async Task AtualizaSenha(string username, UpdateUsuarioDto dto)
        {
            var filter = _userManager.Users.FirstOrDefault(a => a.UserName == username.ToUpper());
            var Tuser = _mapper.Map<Usuario>(dto);
            //   var put = _userManager.ChangePasswordAsync(Tuser, dto.Password, dto.NewPassword);
            // _userManager.UserValidators.Add(put);
            /*var update = _mapper.Map<Usuario>(put);
            await _userManager.UpdateAsync(update);*/

        }

        public async Task GetUser(Usuario user)
        {
            _mapper.Map<ReadUsuarioDto>(user);
            return;
        }
}