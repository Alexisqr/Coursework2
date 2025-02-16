using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrayHome.Infrastructure.Configurations
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasKey(a => a.ID);

            builder.Property(u => u.Sex)
            .IsRequired()
            .HasConversion<string>();
            builder.Property(u => u.TypeAnimal)
           .IsRequired()
           .HasConversion<string>();
            builder.Property(a => a.Age);
            builder.Property(a => a.Sterilization);

            builder.Property(a => a.Name);
            builder.Property(a => a.Description);
            builder.Property(a => a.Photos);
            builder.Property(a => a.IsAvailableForAdoption);


        }
    }
}
