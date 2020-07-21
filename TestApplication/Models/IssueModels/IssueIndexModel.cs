using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Models.IssueModels
{
    public class IssueIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Название задачи")]
        public string Name { get; set; }
        [Display(Name = "Автор задачи")]
        public string ManagerName { get; set; }
        [Display(Name = "Исполнитель задачи")]
        public string ExecutorName { get; set; }
    }
}
