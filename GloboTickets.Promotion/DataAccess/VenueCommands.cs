using GloboTickets.Promotion.DataAccess.Entities;
using GloboTickets.Promotion.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GloboTickets.Promotion.DataAccess
{
    public class VenueCommands
    {
        private readonly PromotionContext repository;

        public VenueCommands(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task AddVenue(Guid venueGuid)
        {
            await repository.GetOrInsertVenue(venueGuid);
            await repository.SaveChangesAsync();
        }

        public async Task SaveVenue(VenueModel venueModel)
        {
            var venue = await repository.GetOrInsertVenue(venueModel.VenueGuid);
            var lastVenueDescription = venue.Descriptions
                .OrderByDescending(description => description.ModifiedDate)
                .FirstOrDefault();
            var modifiedTicks = lastVenueDescription?.ModifiedDate.Ticks ?? 0;
            if (modifiedTicks != venueModel.LastModifiedTicks)
            {
                throw new DbUpdateConcurrencyException("A new update has occurred since you loaded the page. Please refresh and try again.");
            }

            if (lastVenueDescription == null ||
                lastVenueDescription.Name != venueModel.Name ||
                lastVenueDescription.City != venueModel.City)
            {
                await repository.AddAsync(new VenueDescription
                {
                    ModifiedDate = DateTime.UtcNow,
                    Venue = venue,
                    Name = venueModel.Name,
                    City = venueModel.City
                });
                await repository.SaveChangesAsync();
            }
        }
    }
}