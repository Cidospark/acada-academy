using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadaAcademy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcadaAcademy.DataModels
{
    public class AcadaContext : IdentityDbContext<AcadaUser>
    {
        public AcadaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemRoute> ItemRoutes { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Score> Scores { get; set; }
        public virtual DbSet<News> NewsDetails { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }


    }
}
