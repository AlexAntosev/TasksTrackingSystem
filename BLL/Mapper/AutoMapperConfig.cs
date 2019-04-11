using AutoMapper;
using BLL.DTO;
using DAL.Entities;

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
                cfg.CreateMap<TaskDTO, DAL.Entities.Task>()
                    .ForPath(dest => dest.Creator.UserName, opt => opt.MapFrom(src => src.CreatorUserName))
                    .ForPath(dest => dest.Executor.UserName, opt => opt.MapFrom(src => src.ExecutorUserName));
                cfg.CreateMap<TaskDTO, DAL.Entities.Task>().ReverseMap()
                    .ForPath(dest => dest.CreatorUserName, opt => opt.MapFrom(src => src.Creator.UserName))
                    .ForPath(dest => dest.ExecutorUserName, opt => opt.MapFrom(src => src.Executor.UserName));
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<UserDTO, User>().ReverseMap();
                cfg.CreateMap<CommentDTO, Comment>()
                    .ForPath(dest => dest.Author.UserName, opt => opt.MapFrom(src => src.AuthorUserName));
                cfg.CreateMap<CommentDTO, Comment>().ReverseMap()
                    .ForPath(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName));
            });
            Mapper = config.CreateMapper();
        }
    }
}
