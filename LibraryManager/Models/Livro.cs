using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models;

public class Livro
{
    [Key]
    [Required] 
    public int Id { get; set; }
    [Required(ErrorMessage = "O título deve ser obrigatório")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O autor é obrigatório")]

    public string Autor { get; set; }
    public string  Editora { get; set; }

    //Propriedade para relacionar as entidades
    public virtual ICollection<Copia> Copias { get; set; }

}
