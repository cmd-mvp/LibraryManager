using LibraryManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Data
{
    public class UsuarioDbContext :
     IdentityDbContext<Usuario>
    {
        public UsuarioDbContext
            (DbContextOptions<UsuarioDbContext> opts) : base(opts) 
        {

        }
        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            
         builder.Entity<Usuario>()
        .HasMany(u => u.Alugueis)
        .WithOne(a => a.Usuario)
        .HasForeignKey(a => a.UserName);

            base.OnModelCreating(builder);
        }*/
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
