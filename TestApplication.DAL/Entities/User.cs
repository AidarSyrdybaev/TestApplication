using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TestApplication.DAL.Entities
{
    public class User: IdentityUser<int>, IEntity
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string MidlleName { get; set; }

        public List<Issue> Issues { get; set; }
        
        public List<Project> Projects { get; set; }

        public List<UserProject> UserProjects { get; set; }

    }
}
