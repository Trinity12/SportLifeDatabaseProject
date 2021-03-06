﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportLife.Models.IdentityModels {
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim> {
        #region constructors and destructors

        public ApplicationDbContext ()
            : base("name=IdentityConnection") {
        }

        #endregion

        #region methods
        public static ApplicationDbContext Create () {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating ( DbModelBuilder modelBuilder ) {
            base.OnModelCreating(modelBuilder);
            // Map Entities to their tables.
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            // Set AutoIncrement-Properties
            modelBuilder.Entity<User>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Role>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Override some column mappings that do not match our default
            modelBuilder.Entity<User>().Property(r => r.Id).HasColumnName("UserId");
            modelBuilder.Entity<User>().Property(r => r.UserFirstName).HasColumnName("UserFirstName");
            modelBuilder.Entity<User>().Property(r => r.UserSurname).HasColumnName("UserSurname");
        }

        #endregion
    }
}
