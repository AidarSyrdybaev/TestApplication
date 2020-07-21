using System;
using System.Collections.Generic;
using System.Text;
using TestApplication.DAL.Repositories;
using TestApplication.DAL.Repositories.Contracts;

namespace TestApplication.DAL
{
    public class UnitOfWork: IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;

        public IProjectRepository _Projects { get; }
        public IIssueRepository _Issues { get;}
        public IUserProjectRepository _UserProjects { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _Projects = new ProjectRepository(context);
            _Issues = new IssueRepository(context);
            _UserProjects = new UserProjectRepository(context);
        }

        #region Disposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _context.Dispose();

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }
}

