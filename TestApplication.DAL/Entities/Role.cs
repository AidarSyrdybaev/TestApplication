using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TestApplication.DAL.Entities
{
    public class Role: IdentityRole<int>, IEntity
    {
    }
}
