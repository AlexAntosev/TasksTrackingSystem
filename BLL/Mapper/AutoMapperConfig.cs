using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapper
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper;

        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProjectDTO, Project>();
                cfg.CreateMap<ProjectDTO, Project>().ReverseMap();
            });
            Mapper = config.CreateMapper();
        }
    }
}
