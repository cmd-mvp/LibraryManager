using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        [StringLength(14)]
        public string Username { get; set; }
        
        [Required]
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
