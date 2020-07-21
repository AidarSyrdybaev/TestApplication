using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Models.ProjectModels
{
    public class ProjectIndexModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Наименование проекта")]
        public string Name { get; set; }
        public bool IsManager { get; set; }
        public bool IsDirector { get; set; }
        public bool IsEmployee { get; set; }
    }
}
