using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestApplication.Models.IssueModels;

namespace TestApplication.Services.IssueServices.Contracts
{
    public interface IIssueService
    {
        IssueCreateModel GetIssueCreateModel(int ProjectId);
        Task CreateIssue(IssueCreateModel issueCreateModel, ClaimsPrincipal user);
        int DeleteIssue(int Id);
        Task<IssueEditModel> GetIssueEditModel(int Id, ClaimsPrincipal user);
        int IssueEdit(IssueEditModel model);
        IssueDetailsModel GetIssueDetailsModel(int Id);
        IssueIndexListModel GetAllIssueIndex(int ProjectId);
    }
}
