using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication.DAL
{
    public interface IApplicationDbContextFactory
    {
        ApplicationDbContext Create();
    }
}
