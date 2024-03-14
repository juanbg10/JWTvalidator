using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace validacaoJWT.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Entities.UserEntity, Model.UserDto>()
                .ForPath(
                    destino => destino.Id,
                    src => src.MapFrom(src => src.Id))
                .ForPath(
                    destino => destino.UserName,
                    src => src.MapFrom(src => src.UserName));
        }
    }
}
