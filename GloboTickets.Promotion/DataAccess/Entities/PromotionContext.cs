using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTickets.Promotion.DataAccess.Entities
{
    public class PromotionContext : DbContext
    {
        public PromotionContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Act> Act { get; set; }
        public DbSet<Venue> Venue { get; set; }
        public DbSet<Show> Show { get; set; }
        public DbSet<Content> Content { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Act>()
                .HasAlternateKey(act => new { act.ActGuid });

            modelBuilder.Entity<ActRemoved>()
                .HasAlternateKey(actRemoved => new { actRemoved.ActId, actRemoved.RemovedDate });

            modelBuilder.Entity<ActDescription>()
                .HasAlternateKey(actDescription => new { actDescription.ActId, actDescription.ModifiedDate });

            modelBuilder.Entity<Venue>()
                .HasAlternateKey(venue => new { venue.VenueGuid });

            modelBuilder.Entity<Show>()
                .HasAlternateKey(show => new { show.ShowId });

            modelBuilder.Entity<Content>()
                .HasKey(content => content.Hash);
            modelBuilder.Entity<Content>()
                .Property(content => content.Binary)
                .IsRequired();
        }

        public async Task<Act> GetOrInsertAct(Guid actGuid)
        {
            var act = Act
                .Include(act => act.Descriptions)
                .Where(act => act.ActGuid == actGuid)
                .SingleOrDefault();
            if (act == null)
            {
                act = new Act
                {
                    ActGuid = actGuid
                };
                await AddAsync(act);
            }

            return act;
        }

        public async Task<Venue> GetOrInsertVenue(Guid venueGuid)
        {
            var venue = Venue
                .Where(venue => venue.VenueGuid == venueGuid)
                .SingleOrDefault();
            if (venue == null)
            {
                venue = new Venue
                {
                    VenueGuid = venueGuid
                };
                await AddAsync(venue);
            }

            return venue;
        }
    }
}