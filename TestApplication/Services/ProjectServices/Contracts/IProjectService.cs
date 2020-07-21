using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestApplication.Models.ProjectModels;

namespace TestApplication.Services.ProjectServices.Contracts
{
    public interface IProjectService
    {
        Task CreateProjectAsync(ProjectCreateModel projectCreateModel, ClaimsPrincipal user);

        List<ProjectIndexModel> GetProjectIndexModels();

        void EditProject(ProjectEditModel projectEditModel);

        ProjectEditModel GetProjectEditModel(int Id);

        ProjectDetailModel GetProjectDetailModel(int Id);

        SelectList GetUserSelectList(int Id);

        void RemoveUserInProject(int UserId, int ProjectId);

        void Remove(int ProjectId);
    }
}
