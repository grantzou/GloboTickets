using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTickets.Promotion.DataAccess.Entities;
using GloboTickets.Promotion.Models;
using Microsoft.EntityFrameworkCore;

namespace GloboTickets.Promotion.DataAccess
{
    public class VenueQueries
    {
        private readonly PromotionContext repository;

        public VenueQueries(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task<List<VenueModel>> ListVenues()
        {
            var result = await repository.Venue
                .Select(venue => new
                {
                    Venue = venue,
                    Details = venue.Details
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return result
                .Select(row => new VenueModel
                {
                    VenueGuid = row.Venue.VenueGuid,
                    Details = MapVenueDetails(row.Details)
                })
                .ToList();
        }

        public async Task<VenueModel> GetVenue(Guid venueGuid)
        {
            var result = await repository.Venue
                .Where(venue => venue.VenueGuid == venueGuid)
                .Select(venue => new
                {
                    Venue = venue,
                    Details = venue.Details
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .SingleOrDefaultAsync();

            return result == null ? null : new VenueModel
            {
                VenueGuid = result.Venue.VenueGuid,
                Details = MapVenueDetails(result.Details)
            };
        }

        private VenueDetailsModel MapVenueDetails(VenueDetails details)
        {
            return details == null ? null : new VenueDetailsModel
            {
                Name = details.Name,
                City = details.City,
                LastModifiedTicks = details.ModifiedDate.Ticks
            };
        }
    }
}