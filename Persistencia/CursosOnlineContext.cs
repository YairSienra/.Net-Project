using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Persistencia
{
    public class CursosOnlineContext : IdentityDbContext<Usuario>
    {
        public CursosOnlineContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CursoInstructor>().HasKey(ci => new{ci.InstructorId, ci.CursoId});
            modelBuilder.Entity<Tecnicatura>().HasNoKey();
        }

        public DbSet<Comentario> Comentario {get;set;}
        public DbSet<Curso> Curso {get;set;}
        public DbSet<Precio> Precio {get;set;}
        public DbSet<CursoInstructor> CursoInstructor {get;set;}
        public DbSet<Instructor> Instructor {get;set;}
        public DbSet<Tecnicatura> Tecnicatura {get; set;}
       

    }
}