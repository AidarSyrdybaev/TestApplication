using System;
using System.Collections.Generic;
using System.Text;
using TestApplication.DAL.Entities;

namespace TestApplication.DAL.Repositories.Contracts
{
    public interface IProjectRepository: IRepository<Project>
    {
        Project GetAllProject(int Id);
    }
}
