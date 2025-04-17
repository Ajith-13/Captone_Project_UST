using AutoMapper;
using CaptoneProject.Services.ProfileAPI.Models.Dto;

namespace CaptoneProject.Services.ProfileAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProfileDto, Profile>();
                config.CreateMap<Profile, ProfileDto>();

                config.CreateMap<ResponseDto, Profile>();
                config.CreateMap<Profile, ResponseDto>();
            });
            return mappingConfig;
        }
    }
}
