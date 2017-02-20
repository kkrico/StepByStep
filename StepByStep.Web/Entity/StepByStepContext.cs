using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using StepByStep.Web.Models;

namespace StepByStep.Web.Entity
{
    public class StepByStepContext : ApplicationDbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<ApplicationUser>()
                .Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationLogin>().ToTable("UserLogin").HasKey(u => new { u.UserId, u.LoginProvider, u.ProviderKey });
            modelBuilder.Entity<ApplicationRole>().ToTable("Role");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRole");

            modelBuilder.Entity<ApplicationUser>().Ignore(p => p.LockoutEnabled);
            modelBuilder.Entity<ApplicationUser>().Ignore(p => p.TwoFactorEnabled);
        }

        public static StepByStepContext Create()
        {
            return new StepByStepContext();
        }
    }
}