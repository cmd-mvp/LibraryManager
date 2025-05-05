using LibraryManager.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Data.Dtos;

public class UpdateLivroDto
{
  
    [Required(ErrorMessage = "O título deve ser obrigatório")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O autor é obrigatório")]
    public string Autor { get; set; }
    [Required(ErrorMessage = "A editora é obrigatória")]
    public string Editora { get; set; }
}
