using AutoMapper;
using LibraryManager.Data.Dtos;
using LibraryManager.Models;


namespace LibraryManager.Profiles
{
    public class AluguelProfile : Profile
    {
        public AluguelProfile()
        {
            CreateMap<CreateAluguelDto, Aluguel>()
           .ForMember(dest => dest.CopiaId, opt => opt.MapFrom(scr => scr.CopiaId)).ForMember(dest => dest.UserName, opts => opts.MapFrom(scr => scr.UserName));
           // CreateMap<CreateAluguelDto, Aluguel>().ForMember(dest => dest.UserName, opt => opt.MapFrom(scr => scr.UserName));
            CreateMap<Aluguel, ReadAluguelDto>();
        }
    }
}
