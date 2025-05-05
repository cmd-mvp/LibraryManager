using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Data.Dtos
{
    public class ReadCopiaDto
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public bool Disponivel { get; set; } 

    }
}
