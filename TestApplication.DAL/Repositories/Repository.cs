using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestApplication.DAL.Entities;
using TestApplication.DAL.Repositories.Contracts;

namespace TestApplication.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private ApplicationDbContext _context;
        protected DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            var entityEntry = entities.Add(entity);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

        public T GetById(int id)
        {
            return entities.FirstOrDefault(e => e.Id == id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return entities;
        }

        public T Update(T entity)
        {
            var entityEntry = _context.Update(entity);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

        public void Remove(T entity)
        {
            entities.Remove(entity);
            _context.SaveChanges();
        }

        
    }
}
