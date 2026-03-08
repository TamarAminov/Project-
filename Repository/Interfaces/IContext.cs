using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IContext 
    {
        public DbSet<User> Users { get; }
        public DbSet<Event> Events { get; }
        public DbSet<Vendor> Vendors { get; }
        public DbSet<Tasks> Tasks { get; }
        public DbSet<BudgetItem> BudgetItems { get; }
        public DbSet<Area> Area { get; }
        public DbSet<VendorAttribute> VendorAttributes { get; }
        public DbSet<EventType> EventType { get; }
        public DbSet<Category> Category { get; }
        public DbSet<CategoryBudgetRange> CategoryBudgetRanges { get; }
       


        Task<int> save();
    }
}
