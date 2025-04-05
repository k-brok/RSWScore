using System;
using System.Data.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RSW.WebApp.Entities;

namespace RSW.WebApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Association> Associations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SubGroup> SubGroups { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<Patrol> Patrols { get; set; }
        public DbSet<Scout> Scouts { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<SignupCode> SignupCodes { get; set; }
        public DbSet<JurySlot> JurySlots { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Association>()
                .HasMany(e => e.Groups)
                .WithOne(e => e.Association)
                .HasForeignKey(e => e.AssociationId)
                .HasPrincipalKey(e => e.Id);

            builder.Entity<Category>()
                .HasMany(e => e.SubCategories)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .HasPrincipalKey(e => e.Id);

            builder.Entity<SubCategory>()
                .HasMany(e => e.criterias)
                .WithOne(e => e.SubCategory)
                .HasForeignKey(e => e.SubCategoryId)
                .HasPrincipalKey(e => e.Id);

            builder.Entity<Edition>()
                .HasMany(e => e.SubGroups)
                .WithOne(e => e.Edition)
                .HasForeignKey(e => e.EditionId)
                .HasPrincipalKey(e => e.Id);

            builder.Entity<SubGroup>()
                .HasMany(e => e.patrols)
                .WithOne(e => e.SubGroup)
                .HasForeignKey(e => e.SubGroupId)
                .HasPrincipalKey(e => e.Id);

            builder.Entity<Group>()
                .HasMany(e => e.Patrols)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupId)
                .HasPrincipalKey(e => e.Id);

            builder.Entity<Patrol>()
                .HasMany(e => e.Scores)
                .WithOne(e => e.Patrol)
                .HasForeignKey(e => e.PatrolId)
                .HasPrincipalKey(e => e.Id);

            builder.Entity<Criteria>()
                .HasMany(e => e.Scores)
                .WithOne(e => e.Criteria)
                .HasForeignKey(e => e.CriteriaId)
                .HasPrincipalKey(e => e.Id);

            builder.Entity<Patrol>()
                .HasMany(e => e.Scouts)
                .WithOne(e => e.Patrol)
                .HasForeignKey(e => e.PatrolId)
                .HasPrincipalKey(e => e.Id);

            builder.Entity<Edition>()
                .HasMany(e => e.JurySlots)
                .WithOne(e => e.Edition)
                .HasForeignKey(e => e.EditionId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SubGroup>()
                .HasMany(e => e.JurySlots)
                .WithOne(e => e.SubGroup)
                .HasForeignKey(e => e.SubgroupId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Category>()
                .HasMany(e => e.JurySlots)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Patrol>().Property(P => P.TotalScore).HasPrecision(10,7);

            base.OnModelCreating(builder);
        }
    }
}
