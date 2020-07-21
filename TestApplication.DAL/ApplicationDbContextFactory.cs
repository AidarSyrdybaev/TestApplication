using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication.DAL
{
    public class ApplicationDbContextFactory : IApplicationDbContextFactory
    {
        private readonly DbContextOptions _options;
        

        public ApplicationDbContextFactory(
            DbContextOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
          

            _options = options;
           
        }

        public ApplicationDbContext Create()
        {
            return new ApplicationDbContext(_options);
        }
    }
}
