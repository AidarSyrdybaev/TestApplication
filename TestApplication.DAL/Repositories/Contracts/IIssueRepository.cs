using System;
using System.Collections.Generic;
using System.Text;
using TestApplication.DAL.Entities;

namespace TestApplication.DAL.Repositories.Contracts
{
    public interface IIssueRepository:IRepository<Issue>
    {
        List<Issue> GetIssuesAll();
    }
}
