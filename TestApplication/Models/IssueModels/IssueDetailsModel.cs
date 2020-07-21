using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.DAL.Entities;

namespace TestApplication.Models.IssueModels
{
    public class IssueDetailsModel
    {
        public int Id { get; set; }
        [Display(Name = "Название задачи")]
        public string Name { get; set; }
        [Display(Name = "Подробности")]
        public string Content { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Дата конца")]
        public DateTime DeadLine { get; set; }
        [Display(Name = "приоритет проекта")]
        public int Status { get; set; }

        public string ManagerName { get; set; }

        public string ExecutorName { get; set; }
    }
}
