using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.DAL.Entities;
using TestApplication.Other;

namespace TestApplication.Models.ProjectModels
{
    public class ProjectDetailModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Наименование проекта")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Наименование компании заказчика")]
        public string CompanyOrder { get; set; }
        [Required]
        [Display(Name = "Наименование компании исполнителя")]
        public string CompanyExecutor { get; set; }
        
        
        [Required]
        [Display(Name = "Приоритет проекта")]
        public int ProjectPriority { get; set; }
        [Display(Name = "Дата начала проекта")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Дата дедлайна")]
        public DateTime EndDate { get; set; }

        public List<Issue> Issues { get; set; }

        public List<User> Users { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }


    }
}
