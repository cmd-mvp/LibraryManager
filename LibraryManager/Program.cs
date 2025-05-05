using LibraryManager.Authorization;
using LibraryManager.Data;
using LibraryManager.Models;
using LibraryManager.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//string de conexão da minha aplicação e o banco de dados 
var connectionstring = builder.Configuration.GetConnectionString("LivroConnection");

builder.Services.AddDbContext<UsuarioDbContext>
    (opts => 
    {
        opts.UseMySql(connectionstring,
            ServerVersion.AutoDetect(connectionstring));

    });

//adicionar o conceito de identidade para esse usuário, e o papel desse usuário (IdentityRole)
//quem armazenará as configurações desse usuário em si será o UsuarioDbContext com as intruções do AddEntityFrameworkStores
//AddDefaultTokenProviders responsável pela autenticação
builder.Services.AddIdentity<Usuario, IdentityRole>().AddEntityFrameworkStores<UsuarioDbContext>().AddDefaultTokenProviders(); 

//Ativando banco de dados
builder.Services.AddDbContext<LivroContext>(opts => opts.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring)));


//ativando o Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("IdadeMaxima", policy => policy.AddRequirements(new IdadeMaxima(124))
    );
});

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ASIAÇnsilasoijaushiuHSOAASUAI")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

//ativando o Patch
builder.Services.AddControllers().AddNewtonsoftJson();

//a grande vantagem do Swagger é que com ele conseguimos documentar o funcionamento da API +
//. Pessoas que forem utilizar acessando a página do Swagger conseguem ver por exemplo quais são os endpoints disponíveis +
//, qual verbo o endpoint espera para funcionar e fazer determinada operação +
//. Conseguimos validar de maneira visual tudo o que fizemos.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Estamos definindo qual é a informação da API que estamos documentando.+
//O título é "LivroAPI", a versão é "v1"+
//, estamos usando libs internas para pegar o contexto atual e gerar um arquivo XML e temos um caminho baseado no arquivo que estamos gerando e +
//um BaseDirectory baseado no contexto da aplicação que estamos executando.+
//E, por fim, permitimos a execução de comentários XML com esse caminho que estamos criando.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LivrosAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
    options.AddPolicy("My Policy", builder =>
    {
        CorsPolicyBuilder cors =
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true);

        cors.WithOrigins("http://localhost:5173");

        cors.Build();
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("My Policy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
