using System;
using System.Collections.Generic;
using System.Text;
using TestApplication.DAL.Entities;

namespace TestApplication.DAL.Repositories.Contracts
{
    public interface IUserProjectRepository: IRepository<UserProject>
    {
        List<UserProject> GetUserProjectsByUserId(int Id);

        List<UserProject> GetUserProjectsByProjectId(int Id);
    }
}
