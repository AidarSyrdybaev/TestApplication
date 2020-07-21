using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.DAL.Entities;

namespace TestApplication.Models.IssueModels
{
    public class IssueEditModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название задачи")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Комментарий")]
        public string Content { get; set; }
        [Required]
        [Display(Name = "Исполнитель задачи")]
        public int? UserId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        [Required]
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Дата конца")]
        public DateTime DeadLine { get; set; }
        [Required]
        [Display(Name = "статус задачи")]
        public int Status { get; set; }

        public int ManagerId { get; set; }

        public bool IsDirector { get; set; }

        public bool IsManager { get; set; }
    }
}
