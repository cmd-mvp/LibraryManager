
using AutoMapper;
using LibraryManager.Data.Dtos;
using LibraryManager.Models;

namespace LibraryManager.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, ReadUsuarioDto>().
            ForMember(dest => dest.UserName, opt => opt.MapFrom(scr => scr.UserName));
            CreateMap<UpdateUsuarioDto, Usuario>();
            //CreateMap<Task, UpdateUsuarioDto>();
            //CreateMap<Task, Usuario>();
        }
    }
}
