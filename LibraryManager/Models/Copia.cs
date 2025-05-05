using LibraryManager.Data.Dtos;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models;

public class Copia
{
    [Key]
    public int? Id { get; set; }
    public virtual Livro Livro { get; set; }
    public int? LivroId { get; set; }
    public bool Disponivel { get; set; } = true;

}
