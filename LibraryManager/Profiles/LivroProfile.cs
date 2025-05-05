using AutoMapper;
using LibraryManager.Data.Dtos;
using LibraryManager.Models;

namespace LibraryManager.Profiles;

//Classe responsável pelo mapeamento
public class LivroProfile : Profile
{
    public LivroProfile()
    {
        CreateMap<CreateLivroDto, Livro>();
        CreateMap<Livro, ReadLivroDto>();
       // CreateMap<IEnumerable<Livro>, List<Livro>>();
       // CreateMap<Livro, ReadLivroDto>().ForMember(livrodto => livrodto.Copias, opts => opts.MapFrom(livro => livro.Copias));
        CreateMap<UpdateLivroDto, Livro>();
    }
}
