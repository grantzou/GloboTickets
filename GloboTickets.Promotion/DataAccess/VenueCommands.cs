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

        public async Task SetVenueDescription(Guid venueGuid, VenueDescriptionModel venueDescriptionModel)
        {
            var venue = await repository.GetOrInsertVenue(venueGuid);
            var lastVenueDescription = venue.Descriptions
                .OrderByDescending(description => description.ModifiedDate)
                .FirstOrDefault();
            var modifiedTicks = lastVenueDescription?.ModifiedDate.Ticks ?? 0;
            if (modifiedTicks != venueDescriptionModel.LastModifiedTicks)
            {
                throw new DbUpdateConcurrencyException("A new update has occurred since you loaded the page. Please refresh and try again.");
            }

            if (lastVenueDescription == null ||
                lastVenueDescription.Name != venueDescriptionModel.Name ||
                lastVenueDescription.City != venueDescriptionModel.City)
            {
                await repository.AddAsync(new VenueDescription
                {
                    ModifiedDate = DateTime.UtcNow,
                    Venue = venue,
                    Name = venueDescriptionModel.Name,
                    City = venueDescriptionModel.City
                });
                await repository.SaveChangesAsync();
            }
        }
    }
}