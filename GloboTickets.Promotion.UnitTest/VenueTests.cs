using FluentAssertions;
using GloboTickets.Promotion.DataAccess;
using GloboTickets.Promotion.DataAccess.Entities;
using GloboTickets.Promotion.Info;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GloboTickets.Promotion.UnitTest
{
    public class VenueTests
    {
        [Fact]
        public async Task VenuesInitiallyEmpty()
        {
            List<VenueInfo> venues = await venueQueries.ListVenues();
            venues.Should().BeEmpty();
        }

        [Fact]
        public async Task WhenAddVenue_VenueIsReturned()
        {
            var venueGuid = Guid.NewGuid();
            await venueCommands.SaveVenue(VenueModelWith(venueGuid, "American Airlines Center"));

            var venues = await venueQueries.ListVenues();
            venues.Should().Contain(venue => venue.VenueGuid == venueGuid);
        }

        [Fact]
        public async Task WhenAddVenueTwice_OneVenueIsAdded()
        {
            var venueGuid = Guid.NewGuid();
            await venueCommands.SaveVenue(VenueModelWith(venueGuid, "American Airlines Center"));
            await venueCommands.SaveVenue(VenueModelWith(venueGuid, "American Airlines Center"));

            var venues = await venueQueries.ListVenues();
            venues.Count.Should().Be(1);
        }

        [Fact]
        public async Task WhenSetVenueDescription_VenueDescriptionIsReturned()
        {
            var venueGuid = Guid.NewGuid();
            await venueCommands.SaveVenue(VenueModelWith(venueGuid, "American Airlines Center"));

            var venue = await venueQueries.GetVenue(venueGuid);
            venue.Name.Should().Be("American Airlines Center");
        }

        private static VenueInfo VenueModelWith(Guid venueGuid, string name, long lastModifiedTicks = 0)
        {
            return new VenueInfo
            {
                VenueGuid = venueGuid,
                Name = name,
                City = "Dallas, TX",
                LastModifiedTicks = lastModifiedTicks
            };
        }

        private VenueQueries venueQueries;
        private VenueCommands venueCommands;

        public VenueTests()
        {
            var repository = new PromotionContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

            venueQueries = new VenueQueries(repository);
            venueCommands = new VenueCommands(repository);
        }
    }
}