using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestApplication.DAL.Entities;
using TestApplication.Models.UserModels;

namespace TestApplication.Services.UserService.Contract
{
    public interface IUserService
    {
        UserManager<User> _userManager { get; }

        UserInProjectListIndexModel GetUsersInProject(int ProjectId);

        SelectList GetUserSelectList();
        void AddUserInProject(int selectedUserId, int projectId);
        void RemoveUserInProject(int SelectedUserId, int ProjectId);
        Task UserEdit(UserEditModel model);
        UserEditModel GetUserEditModel(int Id);
        UserDetailModel GetUserDetailModel(int Id);
        Task Remove(int Id);
        Task<SelectList> GetManagerSelectList();
    }
}
