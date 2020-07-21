using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication.DAL
{
    public interface IUnitOfWorkFactory
    {
        UnitOfWork Create();
    }
}
