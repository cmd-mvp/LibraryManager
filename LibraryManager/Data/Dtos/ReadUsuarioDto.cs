using LibraryManager.Data;
using LibraryManager.Models;

namespace LibraryManager.Data.Dtos
{
    public class ReadUsuarioDto
    {
        public string UserName { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public IEnumerable<Aluguel> Alugueis { get; set; }
    }
}
