using LibraryManager.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Data.Dtos
{

    public class CreateLivroDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O título deve ser obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O autor é obrigatório")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "A editora é obrigatória")]
        public string Editora { get; set; }
    }
}
