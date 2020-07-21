using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using TestApplication.DAL.Entities;
using TestApplication.Models.IssueModels;
using TestApplication.Models.ProjectModels;
using TestApplication.Models.UserModels;
using TestApplication.Other;

namespace TestApplication
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            ProjectMapping();
            IssueMapping();
            UserMapping();
        }

        private void ProjectMapping()
        {
            CreateMap<ProjectCreateModel, Project>();
            CreateMap<Project, ProjectIndexModel>();
            CreateMap<Project, ProjectDetailModel>().ForMember(target => target.Users,
                src => src.MapFrom(source => source.UserProjects.Select(i => i.User)));
            CreateMap<ProjectEditModel, Project>();
            CreateMap<Project, ProjectEditModel>();
            CreateMap<ProjectDetailModel, Project>().
                ForMember(target => target.ProjectPriority, src => src.MapFrom(target => (int)target.ProjectPriority));
        }

        private void IssueMapping()
        {
            CreateMap<Issue, IssueDetailsModel>().
                ForMember(source => source.ExecutorName, src => src.MapFrom(target => target.User.Surname + " " + target.User.Name)).
                ForMember(target => target.ManagerName, src => src.MapFrom(source => source.Manager.Surname + " " + source.Manager.Name));
            CreateMap<IssueCreateModel,Issue>();
            CreateMap<Issue, IssueIndexModel>().
                ForMember(source => source.ExecutorName, src => src.MapFrom(target => target.User.Surname + " " + target.User.Name)).
                ForMember(target => target.ManagerName, src => src.MapFrom(source => source.Manager.Surname + " " + source.Manager.Name));
            CreateMap<Issue, IssueEditModel>();
            CreateMap<IssueEditModel, Issue>();

        }

        private void UserMapping()
        {
            CreateMap<User, UserIndexModel>();
            CreateMap<User, UserEditModel>();
            CreateMap<User, UserDetailModel>();
        }
    }
}
