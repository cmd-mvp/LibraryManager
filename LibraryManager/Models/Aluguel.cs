using AutoMapper;
using LibraryManager.Data;
using System.ComponentModel.DataAnnotations;
namespace LibraryManager.Models;
public class Aluguel
{
    private LivroContext _context;
    private IMapper _mapper;
    public Aluguel()
    {

    }
    public Aluguel(int copiaId, string status, string username, IMapper mapper)
    {
        CopiaId = copiaId;
        StatusDoAluguel = status;
        UserName = username;
        _mapper = mapper;
    }

    [Key]
    public int Id { get; set; }
    public int CopiaId { get; set; }

    [Required]
    public string UserName { get; set; }
    public DateTime HorarioDoAluguel { get; set; } = DateTime.Now;

    public DateTime Devolucao { get; set; } = DateTime.Now.AddDays(1);

    /*APENAS ESTOU USANDO O ADDMINUTES PARA VERIFICAR SE A LÓGICA DE ALTERAÇÃO DE STATUS ESTÁ CERTA 
    public DateTime Devolucao { get; set; } = DateTime.Now.AddMinutes(5);*/
    public string StatusDoAluguel { get; set; }

    public void Alugar(Copia copia)
    {
            copia.Disponivel = false;
            StatusDoAluguel = StatusAluguel.Pendente.ToString().ToUpper();
            var horarioDoAluguel = HorarioDoAluguel.ToString();
            var devolucao = Devolucao.ToString();
       
    }
    public void VerificaDisponivel(Copia copia)
    {
        if (copia.Disponivel == false)
        {
            throw new Exception("Cópia indisponivel");
        }
        else return;
    }

    public void Devolver(Copia copia)
    {
        copia.Disponivel = true;
        StatusDoAluguel = StatusAluguel.Finalizada.ToString().ToUpper();
    }

}
public enum StatusAluguel { Pendente, Atrasado, Finalizada }


