using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestApplication.DAL.Entities;
using TestApplication.DAL.Repositories.Contracts;

namespace TestApplication.DAL.Repositories
{
    public class UserProjectRepository : Repository<UserProject>, IUserProjectRepository
    {
        public UserProjectRepository(ApplicationDbContext context) : base(context)
        {
            entities = context.UserProjects;
        }

        public List<UserProject> GetUserProjectsByProjectId(int Id)
        {
            return includeUserProjects().Where(i => i.ProjectId == Id).ToList();
        }

        public List<UserProject> GetUserProjectsByUserId(int Id)
        {
            return includeUserProjects().Where(i => i.UserId == Id).ToList();
        }

        private List<UserProject> includeUserProjects()
        {
            return entities.Include(i => i.User).Include(i => i.Project).ToList();
        }
    }
}
