using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Models.IssueModels
{
    public class IssueIndexListModel
    {
        public int ProjectId { get; set; }

        public int? ManagerId { get; set; }

        public List<IssueIndexModel> Issues { get; set; }
    }
}
