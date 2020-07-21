using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using TestApplication.DAL;
using TestApplication.DAL.Entities;
using TestApplication.Models.IssueModels;
using TestApplication.Services.IssueServices.Contracts;
using TestApplication.Services.ProjectServices.Contracts;

namespace TestApplication.Services.IssueServices
{
    public class IssueService: IIssueService
    {
        public UserManager<User> _userManager { get; }

        public RoleManager<Role> _RoleManager { get; }

        private IUnitOfWorkFactory _unitOfWorkFactory { get; }

        public IssueService(UserManager<User> userManager, IUnitOfWorkFactory unitOfWorkFactory, RoleManager<Role> roleManager)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _userManager = userManager;
            _RoleManager = roleManager;
        }

        public IssueIndexListModel GetAllIssueIndex(int ProjectId)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Project = _uow._Projects.GetById(ProjectId);
                var Issues = _uow._Issues.GetIssuesAll();
                var IssuesModels = Mapper.Map<List<IssueIndexModel>>(Issues);
                var Model = new IssueIndexListModel {ProjectId = ProjectId, Issues = IssuesModels, ManagerId = Project.UserId};
                return Model;
            }
        }

        public IssueCreateModel GetIssueCreateModel(int ProjectId)
        {
            return new IssueCreateModel { ProjectId = ProjectId};
        }

        public async Task CreateIssue(IssueCreateModel issueCreateModel, ClaimsPrincipal user)
        {

            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Issue = Mapper.Map<Issue>(issueCreateModel);
                var User = await _userManager.GetUserAsync(user);
                Issue.ManagerId = User.Id;
                _uow._Issues.Create(Issue);
            }
        }

        public int DeleteIssue(int Id)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Issue = _uow._Issues.GetById(Id);
                _uow._Issues.Remove(Issue);
                return Issue.ProjectId;
            }
        }

        public async Task<IssueEditModel> GetIssueEditModel(int Id, ClaimsPrincipal user)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Issue = _uow._Issues.GetById(Id);
                var User = await _userManager.GetUserAsync(user);
                var Model = Mapper.Map <IssueEditModel>(Issue);
                var CheckManager = await _userManager.IsInRoleAsync(User, "manager");
                Model.IsManager = CheckManager && User.Id == Issue.ManagerId;
                Model.IsDirector = await _userManager.IsInRoleAsync(User, "director");
                return Model;
            }
        }

        public int IssueEdit(IssueEditModel model)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Issue = Mapper.Map<DAL.Entities.Issue>(model);
                _uow._Issues.Update(Issue);
                return Issue.ProjectId;
            }
        }

        public IssueDetailsModel GetIssueDetailsModel(int Id)
        {
            using (var _uow = _unitOfWorkFactory.Create())
            {
                var Issue = _uow._Issues.GetById(Id);
                return Mapper.Map<IssueDetailsModel>(Issue);
            }
        }
    }
}
