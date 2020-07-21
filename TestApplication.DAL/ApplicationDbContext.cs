using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApplication.DAL.Entities;

namespace TestApplication.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {   
        public DbSet<Project> Projects { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<UserProject> UserProjects { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Issue>().HasOne(i => i.Manager).WithMany(u => u.Issues).HasForeignKey(i => i.ManagerId);
            builder.Entity<User>().HasMany(u => u.Issues).WithOne(i => i.Manager).HasForeignKey(i => i.ManagerId);
            DisableOneToManyCascadeDelete(builder);
            base.OnModelCreating(builder);
        }

        private void DisableOneToManyCascadeDelete(ModelBuilder builder)
        {
            foreach (var relation in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
