using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class Usuario : IdentityUser
    {
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(14, ErrorMessage = "O campo CPF deve ter 14 caracteres")]
        [Key]
        public string CPF { get; set; }
        public virtual ICollection<Aluguel> Alugueis { get; set; }  
        public Usuario() : base()
        {

        }
    }
}
