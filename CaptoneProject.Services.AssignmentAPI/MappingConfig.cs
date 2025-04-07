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
                config.CreateMap<AssignmentQuestion, AssignmentQuestionDto>();
                config.CreateMap<AssignmentQuestionDto, AssignmentQuestion>();

                config.CreateMap<AssignmentQuestionResponseDto, AssignmentQuestion>();
                config.CreateMap<AssignmentQuestion, AssignmentQuestionResponseDto>();
            });
            return mappingConfig;
        }
    }
}
