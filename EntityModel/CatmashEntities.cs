using Microsoft.EntityFrameworkCore;

namespace Catmash.EntityModel
{
    public class CatmashEntities : DbContext
    {
        public DbSet<Image> Images { get; set; }

        public CatmashEntities(DbContextOptions<CatmashEntities> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Image>()
                .HasKey(image => image.Id);
            modelBuilder.Entity<Image>()
                .Property(image => image.Url)
                .IsRequired();
            modelBuilder.Entity<Image>()
                .Property(image => image.Score)
                .IsRequired();
            modelBuilder.Entity<Image>()
                .HasIndex(image => image.Id);
        }
    }
}