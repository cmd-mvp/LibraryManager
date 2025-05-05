using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LibraryManager.Authorization
{
    // classe responsável por interceptar as informações
    // do token e reconhecida como um gerenciador de acesso pelo .NET.
    //Crio uma variavel para adicionar uma claim no tokenService
    //e relaciono ela com a data de nascimento 
    public class IdadeAuthorization : AuthorizationHandler<IdadeMaxima>
    {
        protected override Task 
            HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMaxima requirement)
        {
            var dataNascimentoClaim = context
                .User.FindFirst(claim=>claim.Type == ClaimTypes.DateOfBirth);

            if (dataNascimentoClaim is null) return Task.CompletedTask;

            var dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);

            var idadeUsuario = DateTime.Today.Year - dataNascimento.Year;

            if (dataNascimento > DateTime.Today.AddYears(-idadeUsuario))
                idadeUsuario--;

            if (idadeUsuario >= requirement.Idade)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
