using System;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapper
{
    public sealed class AutoMapperConfig
    {
        public static IMapper Mapper;

        public static MapperConfiguration CreateConfigure()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProjectDTO, Project>();
                cfg.CreateMap<ProjectDTO, Project>().ReverseMap();

                cfg.CreateMap<TaskDTO, DAL.Entities.Task>()
                    .ForPath(dest => dest.Creator.UserName, opt => opt.MapFrom(src => src.CreatorUserName))
                    .ForPath(dest => dest.Executor.UserName, opt => opt.MapFrom(src => src.ExecutorUserName))
                    .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => Convert.ToDateTime(src.Deadline)))
                    .ForMember(dest => dest.Created, opt => opt.MapFrom(src => Convert.ToDateTime(src.Created)))
                    .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => Convert.ToDateTime(src.Updated)));
                cfg.CreateMap<TaskDTO, DAL.Entities.Task>().ReverseMap()
                    .ForPath(dest => dest.CreatorUserName, opt => opt.MapFrom(src => src.Creator.UserName))
                    .ForPath(dest => dest.ExecutorUserName, opt => opt.MapFrom(src => src.Executor.UserName))
                    .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline.ToString()))
                    .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToString()))
                    .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated.ToString()));

                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<UserDTO, User>().ReverseMap();

                cfg.CreateMap<CommentDTO, Comment>()
                    .ForPath(dest => dest.Author.UserName, opt => opt.MapFrom(src => src.AuthorUserName))
                    .ForMember(dest => dest.Time, opt => opt.MapFrom(src => Convert.ToDateTime(src.Time)));
                cfg.CreateMap<CommentDTO, Comment>().ReverseMap()
                    .ForPath(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName))
                    .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time.ToString()));

                cfg.CreateMap<InviteDTO, Invite>()
                    .ForPath(dest => dest.Author.UserName, opt => opt.MapFrom(src => src.AuthorUserName))
                    .ForPath(dest => dest.Receiver.UserName, opt => opt.MapFrom(src => src.ReceiverUserName))
                    .ForPath(dest => dest.Project.Name, opt => opt.MapFrom(src => src.ProjectName))
                    .ForMember(dest => dest.Time, opt => opt.MapFrom(src => Convert.ToDateTime(src.Time)));
                cfg.CreateMap<InviteDTO, Invite>().ReverseMap()
                    .ForPath(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName))
                    .ForPath(dest => dest.ReceiverUserName, opt => opt.MapFrom(src => src.Receiver.UserName))
                    .ForPath(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
                    .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time.ToString()));
            });
        }

    }
}
