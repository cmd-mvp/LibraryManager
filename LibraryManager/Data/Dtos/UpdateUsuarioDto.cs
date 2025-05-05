using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Data.Dtos
{
    public class UpdateUsuarioDto
    {
        public string UserName { get; set; }
        public DateTime DataNascimento { get; set; }
        /*  public string CPF { get; set; }
          [DataType(DataType.Password)]
          public string Password { get; set; }

          [DataType(DataType.Password)]
          public string NewPassword { get; set; }
          [Compare("NewPassword")]
          public string ReNewPassword { get; set; }*/
    }
}
