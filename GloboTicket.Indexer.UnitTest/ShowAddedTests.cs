using FluentAssertions;
using GloboTicket.Promotion.Messages.Acts;
using GloboTicket.Promotion.Messages.Shows;
using GloboTicket.Promotion.Messages.Venues;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GloboTicket.Indexer.UnitTest
{
    public class ShowAddedTests
    {
        private readonly InMemoryRepository repository;
        private readonly ShowAddedHandler showAddedHandler;

        public ShowAddedTests()
        {
            repository = new InMemoryRepository();
            showAddedHandler = new ShowAddedHandler(repository);
        }

        [Fact]
        public async Task WhenShowIsAdded_ShowIsInIndex()
        {
            var showAdded = new ShowAdded
            {
                act = new ActRepresentation
                {
                    actGuid = Guid.NewGuid(),
                    description = new ActDescriptionRepresentation
                    {
                        title = "New Act",
                        imageHash = "abc123",
                        modifiedDate = DateTime.UtcNow
                    }
                },
                venue = new VenueRepresentation
                {
                    venueGuid = Guid.NewGuid(),
                    description = new VenueDescriptionRepresentation
                    {
                        name = "New Venue",
                        city = "Anytown, VA",
                        modifiedDate = DateTime.UtcNow
                    },
                    location = new VenueLocationRepresentation
                    {
                        latitude = 123,
                        longitude = -45,
                        modifiedDate = DateTime.UtcNow
                    },
                    timeZone = new VenueTimeZoneRepresentation
                    {
                        timeZone = "UTC",
                        modifiedDate = DateTime.UtcNow
                    }
                }
            };
            await showAddedHandler.Handle(showAdded);

            repository.Shows.Should().BeEquivalentTo(new List<ShowAdded> { showAdded });
        }
    }
}
