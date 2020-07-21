using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestApplication.DAL.Entities;
using TestApplication.DAL.Repositories.Contracts;

namespace TestApplication.DAL.Repositories
{
    public class IssueRepository: Repository<Issue>, IIssueRepository
    {
        public IssueRepository(ApplicationDbContext context) : base(context)
        {
            entities = context.Issues;
        }

        public List<Issue> GetIssuesAll()
        {
            return entities.Include(i => i.Manager).Include(i => i.User).ToList();
        }
    }
}
