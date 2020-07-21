using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestApplication.DAL;
using TestApplication.DAL.Entities;
using TestApplication.Models.UserModels;
using TestApplication.Other;
using TestApplication.Services.UserService.Contract;

namespace TestApplication.Services.UserServices
{
    public class UserService: IUserService
    {   
        public UserManager<User> _userManager { get; }

        private IUnitOfWorkFactory _unitOfWorkFactory { get; }

        public UserService(IUnitOfWorkFactory unitOfWorkFactory, UserManager<User> userManager)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _userManager = userManager;
        }

        public UserInProjectListIndexModel GetUsersInProject(int ProjectId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var Project = uow._Projects.GetById(ProjectId);
                var Users = uow._UserProjects.GetUserProjectsByProjectId(ProjectId).Select(i => i.User).ToList();
                var UserModels = Mapper.Map <List<UserIndexModel>>(Users);
                var UsersInProject = new UserInProjectListIndexModel
                    {userIndexModels = UserModels, ProjectId = ProjectId, ManagerId = Project.UserId};
                return UsersInProject;
            }
        }

        public SelectList GetUserSelectList()
        {
            SelectList userSelectList = new SelectList(_userManager.Users, "Id", "UserName");
            return userSelectList;
        }

        public void AddUserInProject(int UserId, int ProjectId)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {

                var checkUserProject =
                    _uow._UserProjects.GetAll().Any(i => i.UserId == UserId && i.ProjectId == ProjectId);
                if (!checkUserProject)
                    _uow._UserProjects.Create(new UserProject { UserId = UserId, ProjectId = ProjectId });
            }

        }

        public void RemoveUserInProject(int UserId, int ProjectId)
        {
            using(var _uow = _unitOfWorkFactory.Create())
            {
                var UserProject =
                    _uow._UserProjects.GetAll().FirstOrDefault(i => i.UserId == UserId && i.ProjectId == ProjectId);
                foreach (var issue in _uow._Issues.GetAll().ToList())
                {
                    if (issue.ProjectId == ProjectId && issue.UserId == UserId)
                    {
                        issue.UserId = null;
                        issue.Status = (int)IssueStatusEnum.ToDo;  
                        _uow._Issues.Update(issue);
                    }
                }
                if (UserProject != null)
                    _uow._UserProjects.Remove(UserProject);
            }
        }

        public async Task UserEdit(UserEditModel model)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var User = _userManager.Users.ToList().FirstOrDefault(u => u.Id == model.Id);
                User.Name = model.Name;
                User.MidlleName = model.MidlleName;
                User.Surname = model.Surname;
                await _userManager.UpdateAsync(User);
            }
        }

        public UserEditModel GetUserEditModel(int Id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var User = _userManager.Users.FirstOrDefault(u => u.Id == Id);
                return Mapper.Map<UserEditModel>(User);
            }
        }

        public UserDetailModel GetUserDetailModel(int Id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var User = _userManager.Users.FirstOrDefault(u => u.Id == Id);
                return Mapper.Map<UserDetailModel>(User);
            }
        }

        public async Task Remove(int Id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var User = await _userManager.FindByIdAsync(Id.ToString());

                foreach (var userProject in uow._UserProjects.GetAll().ToList())
                {
                    if(userProject.UserId == User.Id)
                        uow._UserProjects.Remove(userProject);
                }

                foreach (var issue in uow._Issues.GetAll().ToList())
                {
                    if (issue.UserId == User.Id)
                    {
                        issue.UserId = null;
                        uow._Issues.Update(issue);
                    }
                    if(issue.ManagerId == User.Id)
                    {
                        uow._Issues.Remove(issue);
                    }
                }

                if (await _userManager.IsInRoleAsync(User, "manager") ||
                    await _userManager.IsInRoleAsync(User, "director"))
                {
                    foreach (var Project in uow._Projects.GetAll().ToList())
                    {
                        if (Project.UserId == User.Id)
                        {
                            Project.UserId = null;
                            uow._Projects.Update(Project);
                        }

                    }
                }
            }
        }

        public async Task<SelectList> GetManagerSelectList()
        {   
            List<User> users = new List<User>();
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, "manager"))
                {
                    users.Add(user);
                }
            }
            return new SelectList(users, "Id", "UserName");
        }
    }
}
