using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication.DAL.Entities
{
    public class Project: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CompanyOrder { get; set; }

        public string CompanyExecutor { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public int ProjectPriority { get; set; }

        public List<Issue> Issues { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<UserProject> UserProjects { get; set; }


    }
}
