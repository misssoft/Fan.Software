using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Software.DomainModels;
using Software.Models;

namespace Software.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Topic> Topics { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<GroupWork> Works { get; set; }

        public DbSet<GroupAssignment> Assignments { get; set; }

       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GroupAssignment>().HasKey(x => new
            {
                x.MemberId,
                x.WorkId
            });

            builder.Entity<GroupAssignment>()
           .HasOne(p => p.Work)
           .WithMany()
           .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

    }
}
