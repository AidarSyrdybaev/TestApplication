using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication.DAL.Entities
{
    public class Issue : IEntity
    {
        public int Id { get ; set ; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DeadLine { get; set; }

        public int Status { get; set; }

        public int ManagerId { get; set; }

        public User Manager { get; set; }
    }
}
