using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Data;

public class LivroContext : DbContext
{
    //construtor vai receber uma configuração, as opções de acesso ao banco desse contexto
    public LivroContext(DbContextOptions<LivroContext> opts) : base(opts)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        
      /*  builder.Entity<Copia>().HasOne(copia => copia.Livro).WithMany(livro => livro.Copias).HasForeignKey(copia => copia.LivroId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Livro>().HasMany(livro=>livro.Copias).WithOne(copia=>copia.Livro).HasPrincipalKey(livro=>livro.Id);
        base.OnModelCreating(builder);*/

    }


    //propriedade que vai dar acesso aos livros da base de dados.
    public DbSet<Livro> Livros { get; set; }

    //propriedade que vai dar acesso as copias da base de dados.
    public DbSet<Copia> Copias { get; set; }
    public DbSet<Aluguel> Alugueis { get; set; }
}
