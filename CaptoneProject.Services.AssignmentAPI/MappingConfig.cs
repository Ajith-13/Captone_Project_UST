using AutoMapper;
using CaptoneProject.Services.AssignmentAPI.Data.Dto.Assignment;
using CaptoneProject.Services.AssignmentAPI.Models;

namespace CaptoneProject.Services.AssignmentAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Assignment, AssignmentDto>();
                config.CreateMap<AssignmentDto, Assignment>();

                config.CreateMap<AssignmentResponseDto, Assignment>();
                config.CreateMap<Assignment, AssignmentResponseDto>();
            });
            return mappingConfig;
        }
    }
}
