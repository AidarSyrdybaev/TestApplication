using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.Other;

namespace TestApplication.Models.ProjectModels
{
    public class ProjectCreateModel
    {
        [Required]
        [Display(Name = "Наименование проекта")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Наименование компании заказчика")]
        public string CompanyOrder { get; set; }
        [Required]
        [Display(Name = "Наименование компаниии исполнителя")]
        public string CompanyExecutor { get; set; }
        [Required]
        [Display(Name = "Приоритет проекта")]
        public int ProjectPriority { get; set; }
        [Required]
        [Display(Name = "Дата начала проекта")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Дата дедлайна")]
        public DateTime EndDate { get; set; }
        public int ManagerId { get; set; }
    }
}
