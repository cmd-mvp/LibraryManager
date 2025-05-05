using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Data.Dtos
{
    public class CreateAluguelDto
    {
       /*[Key]
        public int Id { get; set; }*/
        public int CopiaId { get; set; }
        public string UserName { get; set; }


    }
}
