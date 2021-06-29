using System;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.School.Entity
{
    public class StudentsContext: DbContext
    {
        //contexto de la base de datos
        public DbSet<Students> students { get; set; }

        public StudentsContext()
        {
        }

        public StudentsContext(DbContextOptions options) : base(options) { }
    }
}
