using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practice.Domain.Entities;

namespace Practice.Application.Contracts.Persistence
{
    public interface IStrayHomeContext
    {
        public DbSet<Animal> Animals { get; set; }


        /// <summary>
        /// Saves changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        Task<int> SaveChangesAsync();
    }
}
