using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.DAL.Entities;

namespace TestApplication.Models.IssueModels
{
    public class IssueCreateModel
    {   
        [Required]
        [Display(Name = "Название задачи")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Комментарий")]
        public string Content { get; set; }
        [Required]
        [Display(Name = "Исполнитель задачи")]
        public int? UserId { get; set; }

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
    }
}
