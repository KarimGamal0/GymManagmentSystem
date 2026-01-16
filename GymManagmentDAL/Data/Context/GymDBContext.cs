using GymManagmentDAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Context
{
    public class GymDBContext : IdentityDbContext<ApplicationUser>                                    //old-one -->  //DbContext
    {
        #region Old ConnectionString Connection
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=GymManagmentG02;Trusted_Connection=true;TrustServerCertificate=True");
        //}
        #endregion


        public GymDBContext(DbContextOptions<GymDBContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all Configuration Class
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(E =>
            {
                E.Property(X => X.FirstName).HasColumnType("varchar").HasMaxLength(50);
                E.Property(X => X.LastName).HasColumnType("varchar").HasMaxLength(50);

            });

            
        }

        #region DBSet
        public DbSet<Member> Members { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }

        #endregion
    }
}
