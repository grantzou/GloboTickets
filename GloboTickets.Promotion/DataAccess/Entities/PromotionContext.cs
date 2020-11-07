using Microsoft.EntityFrameworkCore;

namespace GloboTickets.Promotion.DataAccess.Entities
{
    public class PromotionContext : DbContext
    {
        public PromotionContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Act> Act { get; set; }
        public DbSet<Content> Content { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Act>()
                .HasAlternateKey(act => new { act.ActGuid });

            modelBuilder.Entity<ActRemoved>()
                .HasAlternateKey(actRemoved => new { actRemoved.ActId, actRemoved.RemovedDate });

            modelBuilder.Entity<ActDescription>()
                .HasAlternateKey(actDescription => new { actDescription.ActId, actDescription.ModifiedDate });

            modelBuilder.Entity<Content>()
                .HasKey(content => content.Hash);
            modelBuilder.Entity<Content>()
                .Property(content => content.Binary)
                .IsRequired();
        }
    }
}