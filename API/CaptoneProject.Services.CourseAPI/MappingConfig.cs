using AutoMapper;
using CaptoneProject.Services.CourseAPI.Data.Dto.Course;
using CaptoneProject.Services.CourseAPI.Data.Dto.Module;
using CaptoneProject.Services.CourseAPI.Models;

namespace CaptoneProject.Services.CourseAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Course, CourseDto>();
                config.CreateMap<CourseDto, Course>();

                config.CreateMap<Course, CourseResponseDto>();
                config.CreateMap<CourseResponseDto, Course>();

                config.CreateMap<ModuleDto, Module>().ForMember(dest => dest.Course, opt => opt.Ignore());
                config.CreateMap<Module, ModuleDto>();

                config.CreateMap<Module, ModuleResponseDto>();
                config.CreateMap<ModuleResponseDto, Module>();
            });
            return mappingConfig;
        }
    }
}
