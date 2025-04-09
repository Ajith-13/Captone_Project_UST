using AutoMapper;
using CaptoneProject.Services.NotesAPI.Models;
using CaptoneProject.Services.NotesAPI.Models.Dto;

namespace CaptoneProject.Services.NotesAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<NotesDto, Notes>();
                config.CreateMap<Notes, NotesDto>();

                config.CreateMap<NotesResponseDto, Notes>();
                config.CreateMap<Notes, NotesResponseDto>();
            });
            return mappingConfig;
        }
    }
}
