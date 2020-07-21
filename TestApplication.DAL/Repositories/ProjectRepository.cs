using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestApplication.DAL.Entities;
using TestApplication.DAL.Repositories.Contracts;

namespace TestApplication.DAL.Repositories
{
    public class ProjectRepository: Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context): base(context)
        {
            entities = context.Projects;
        }

        public Project GetAllProject(int Id)
        {
            return entities.Where(i => i.Id == Id).Include(i => i.User).Include(i => i.UserProjects).Include(i => i.Issues).First();
        }
    }
}
