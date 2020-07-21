using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Models.UserModels
{
    public class UserInProjectListIndexModel
    {
        public List<UserIndexModel> userIndexModels { get; set; }

        public int ProjectId { get; set; }

        public int? ManagerId { get; set; }
    }
}
