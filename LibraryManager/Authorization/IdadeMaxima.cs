using Microsoft.AspNetCore.Authorization;

namespace LibraryManager.Authorization
{
    public class IdadeMaxima : IAuthorizationRequirement
    {
        public IdadeMaxima(int idade)
        {
            Idade = idade;
        }
        public int Idade { get; set; }
    }
}
