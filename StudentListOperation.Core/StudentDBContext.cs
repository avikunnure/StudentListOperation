using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentListOperation.Core.Entity;

namespace StudentListOperation.Core
{
    public class StudentDBContext:DbContext
    {
        public StudentDBContext() : base()
        {

        }
        public StudentDBContext(DbContextOptions<StudentDBContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Student> students { get; set; }
        public DbSet<StudentSubjects> StudentSubjects { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subjects>().HasData(Data.SubjectsData.GetSubjects());
            base.OnModelCreating(modelBuilder);
        }
    }
}
