using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Plaswijzer.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Plaswijzer.Data
{
    public class ToiletContext : DbContext
    {
        public ToiletContext(DbContextOptions<ToiletContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Toilet>(MapToilet);
        }

        private void MapToilet(EntityTypeBuilder<Toilet> obj)
        {
            obj.ToTable("Toilet");
            
        }

        public DbSet<DogToilet> DogToilets { get; set; }
        public DbSet<GehandToilet> GehandToilets { get; set; }
        public DbSet<Toilet> Toilets { get; set; }
        public DbSet<Urinoir> Urinoirs { get; set; }

    }
}
