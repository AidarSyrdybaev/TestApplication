using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Models.UserModels
{
    public class UserIndexModel
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MidlleName { get; set; }

        public int ProjectId { get; set; }
    }
}
