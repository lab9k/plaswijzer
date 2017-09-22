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
            builder.Entity<Urinoir>(MapUri);
            builder.Entity<GehandToilet>(MapGehand);
            builder.Entity<DogToilet>(MapDog);
        }

        private void MapToilet(EntityTypeBuilder<Toilet> obj)
        {
            obj.ToTable("Toilet");
        }

        private void MapUri(EntityTypeBuilder<Urinoir> obj)
        {
            obj.ToTable("Urinoir");
        }

        private void MapGehand(EntityTypeBuilder<GehandToilet> obj)
        {
            obj.ToTable("GehandToilet");
        }

        private void MapDog(EntityTypeBuilder<DogToilet> obj)
        {
            obj.ToTable("DogToilet");
        }

        public DbSet<DogToilet> DogToilets { get; set; }
        public DbSet<GehandToilet> GehandToilets { get; set; }
        public DbSet<Toilet> Toilets { get; set; }
        public DbSet<Urinoir> Urinoirs { get; set; }

    }
}
