using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using obg.Domain.Entities;

namespace obg.DataAccess.Context
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<Petition> Petitions { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseLine> PurchaseLines { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public Context() { }
        public Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Name);
            //modelBuilder.Entity<Administrator>().HasKey(a => a.Name);
            //modelBuilder.Entity<Owner>().HasKey(o => o.Name);
            //modelBuilder.Entity<Employee>().HasKey(e => e.Name);
            modelBuilder.Entity<Pharmacy>().HasKey(p => p.Name);
            modelBuilder.Entity<Medicine>().HasKey(m => m.Code);
            modelBuilder.Entity<Invitation>().HasKey(i => i.IdInvitation);
            modelBuilder.Entity<Demand>().HasKey(d => d.IdDemand);
            modelBuilder.Entity<Petition>().HasKey(p => p.IdPetition);
            modelBuilder.Entity<Purchase>().HasKey(p => p.IdPurchase);
            modelBuilder.Entity<PurchaseLine>().HasKey(p => p.IdPurchaseLine);
            modelBuilder.Entity<Session>().HasKey(s => s.IdSession);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory(); // ¿System.IO?

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString(@"Context");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}