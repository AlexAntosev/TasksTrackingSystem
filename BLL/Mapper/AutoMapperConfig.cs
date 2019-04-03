using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public sealed class AutoMapperConfig
    {
        public static IMapper Mapper;

        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProjectDTO, Project>();
                cfg.CreateMap<ProjectDTO, Project>().ReverseMap();
                cfg.CreateMap<TaskDTO, DAL.Entities.Task>();
                cfg.CreateMap<TaskDTO, DAL.Entities.Task>().ReverseMap();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<UserDTO, User>().ReverseMap();
            });
            Mapper = config.CreateMapper();
        }
    }
}
