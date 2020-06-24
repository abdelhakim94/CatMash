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
                .HasIndex(image => image.Id);
            modelBuilder.Entity<Image>()
                .Property(image => image.Url)
                .IsRequired();
            modelBuilder.Entity<Image>()
                .Property(image => image.Score)
                .IsRequired()
                .HasColumnType("decimal(10, 5)");
            modelBuilder.Entity<Image>()
                .Property(image => image.Votes)
                .IsRequired();
        }
    }
}