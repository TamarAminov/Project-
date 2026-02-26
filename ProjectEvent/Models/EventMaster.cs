using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEvent.Models
{
    public class EventMaster : DbContext, IContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<BudgetItem> BudgetItems { get; set; }


        public virtual DbSet<Vendor> Vendors { get; set; }

        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<VendorAttribute> VendorAttributes { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<Category> Category { get; set; }




        DbSet<Event> IContext.Events => Events;
        DbSet<Vendor> IContext.Vendors => Vendors;

        DbSet<Tasks> IContext.Tasks => Tasks;

        DbSet<BudgetItem> IContext.BudgetItems => BudgetItems;

        DbSet<User> IContext.Users => Users;
        DbSet<Area> IContext.Area => Area;
        DbSet<VendorAttribute> IContext.VendorAttributes => VendorAttributes;
        DbSet<EventType> IContext.EventType => EventType;
        DbSet<Category> IContext.Category => Category;



        public async Task<int> save()
        {
            return await this.SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasks>()
            .HasOne(t => t.AllEvent)
            .WithMany(e => e.Tasks)
            .HasForeignKey(t => t.EventID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BudgetItem>()
           .HasOne(t => t.AllEvent)
           .WithMany(e => e.BudgetItems)
           .HasForeignKey(t => t.EventID)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
            .HasMany(e => e.Vendors)
           .WithMany(v => v.Events)
           .UsingEntity(j => j.ToTable("EventVendors"));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("server=sql;database=EventDB;trusted_connection=true;TrustServerCertificate=True");
            //optionsBuilder.UseSqlServer("server=.\\MSSQLSERVER;database=EventDB;trusted_connection=true;TrustServerCertificate=True");
            optionsBuilder.UseSqlServer("server=.;database=EventDB;trusted_connection=true;TrustServerCertificate=True");
        }


    }
}

