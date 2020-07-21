using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TestApplication.DAL;
using TestApplication.DAL.Entities;
using TestApplication.Models.ProjectModels;
using TestApplication.Services.ProjectServices.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestApplication.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        public UserManager<User> _userManager { get; }

        private IUnitOfWorkFactory _unitOfWorkFactory { get; }

        public ProjectService(UserManager<User> userManager, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _userManager = userManager;
        }

        public async Task CreateProjectAsync(ProjectCreateModel projectCreateModel, ClaimsPrincipal user)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Project = Mapper.Map<Project>(projectCreateModel);
                var User = await _userManager.GetUserAsync(user);
                Project.UserId = User.Id;
                _uow._Projects.Create(Project);
            }
        }

        public void CreateProject(ProjectCreateModel projectCreateModel)
        {
            throw new NotImplementedException();
        }

        public List<ProjectIndexModel> GetProjectIndexModels()
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var ProjectModels = Mapper.Map<List<ProjectIndexModel>>(_uow._Projects.GetAll());
                return ProjectModels;

            }
        }

        public void EditProject(ProjectEditModel projectEditModel)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Project = Mapper.Map<Project>(projectEditModel);
                _uow._Projects.Update(Project);

            }
        }

        public ProjectEditModel GetProjectEditModel(int Id)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Project = _uow._Projects.GetAllProject(Id);
                var ProjectModel = Mapper.Map<ProjectEditModel>(Project);
                return ProjectModel;
            }
        }

        public ProjectDetailModel GetProjectDetailModel(int Id)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Project = _uow._Projects.GetAllProject(Id);
                Project.UserProjects = _uow._UserProjects.GetUserProjectsByProjectId(Id);
                var ProjectModel = Mapper.Map<ProjectDetailModel>(Project);
                return ProjectModel;
            }
        }

        public SelectList GetUserSelectList(int Id)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {

                var Users = _uow._UserProjects.GetUserProjectsByProjectId(Id).Select(i => i.User);
                SelectList userSelectList = new SelectList(Users, "Id", "UserName");
                return userSelectList;
            }
        }

        

        public void RemoveUserInProject(int UserId, int ProjectId)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {

                var UserProject =
                    _uow._UserProjects.GetAll().FirstOrDefault(i => i.UserId == UserId && i.ProjectId == ProjectId);
                if (UserProject != null)
                    _uow._UserProjects.Remove(UserProject);
            }
        }

        public void Remove(int ProjectId)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Project = _uow._Projects.GetById(ProjectId);
                foreach (var issue in _uow._Issues.GetAll().ToList())
                {
                    if(issue.ProjectId == Project.Id)
                        _uow._Issues.Remove(issue);

                }

                foreach (var Uproj in _uow._UserProjects.GetAll().ToList())
                {
                    if(Uproj.ProjectId == ProjectId)
                        _uow._UserProjects.Remove(Uproj);
                }
                _uow._Projects.Remove(Project);
            }
        }
    }
}
