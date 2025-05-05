using AutoMapper;
using LibraryManager.Data.Dtos;
using LibraryManager.Models;

namespace LibraryManager.Profiles
{
    public class CopiaProfile : Profile
    {
        public CopiaProfile()
        {
            CreateMap<CreateCopiaDto, Copia>();
            CreateMap<Copia, ReadCopiaDto>();
        }
    }
}
