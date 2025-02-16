using Microsoft.EntityFrameworkCore;
using Practice.Application.Contracts.Persistence;
using Practice.Domain.Entities;
using StrayHome.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrayHome.Infrastructure.Data
{
    public class StrayHomeContext : DbContext, IStrayHomeContext
    {
        public DbSet<Animal> Animals { get; set; }
      

        public StrayHomeContext(DbContextOptions<StrayHomeContext> options)
      : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AnimalConfiguration());

        }

        void IStrayHomeContext.SaveChanges()
        {
            SaveChanges();
        }

        Task<int> IStrayHomeContext.SaveChangesAsync()
        {
            return SaveChangesAsync();
        }
    }
}
