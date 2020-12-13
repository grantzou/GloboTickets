using FluentAssertions;
using GloboTicket.Indexer.Handlers;
using GloboTicket.Indexer.Updaters;
using GloboTicket.Promotion.Messages.Acts;
using GloboTicket.Promotion.Messages.Shows;
using GloboTicket.Promotion.Messages.Venues;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GloboTicket.Indexer.UnitTest
{
    public class HandlerTests
    {
        private readonly InMemoryRepository repository;
        private readonly ShowAddedHandler showAddedHandler;
        private readonly ActDescriptionChangedHandler actDescriptionChangedHandler;
        private readonly VenueDescriptionChangedHandler venueDescriptionChangedHandler;

        private readonly Guid actGuid = Guid.NewGuid();
        private readonly Guid venueGuid = Guid.NewGuid();

        public HandlerTests()
        {
            repository = new InMemoryRepository();
            var actUpdater = new ActUpdater(repository);
            var venueUpdater = new VenueUpdater(repository);
            showAddedHandler = new ShowAddedHandler(repository, actUpdater, venueUpdater);
            actDescriptionChangedHandler = new ActDescriptionChangedHandler(repository, actUpdater);
            venueDescriptionChangedHandler = new VenueDescriptionChangedHandler(repository, venueUpdater);
        }

        [Fact]
        public async Task WhenShowIsAdded_ShowIsInIndex()
        {
            var showAdded = GivenShowAdded(
                actTitle: "Expected act title",
                venueName: "Expected venue name");
            await showAddedHandler.Handle(showAdded);

            repository.Shows.Single().ActDescription.Title.Should().Be("Expected act title");
            repository.Shows.Single().VenueDescription.Name.Should().Be("Expected venue name");
        }

        [Fact]
        public async Task WhenActDescriptionIsChangedAfterShowIsAdded_ThenShowIsUpdated()
        {
            var showAdded = GivenShowAdded(actTitle: "Original Title", actDescriptionAge: 1);
            var actDescriptionChanged = GivenActDescriptionChanged(actTitle: "Modified Title");

            await showAddedHandler.Handle(showAdded);
            await actDescriptionChangedHandler.Handle(actDescriptionChanged);

            repository.Shows.Single().ActDescription.Title.Should().Be("Modified Title");
        }

        [Fact]
        public async Task WhenActDescriptionChangeArrivesBeforeShowAdded_ThenShowUsesLatestDecsription()
        {
            var showAdded = GivenShowAdded(actTitle: "Original Title", actDescriptionAge: 1);
            var actDescriptionChanged = GivenActDescriptionChanged(actTitle: "Modified Title");

            await actDescriptionChangedHandler.Handle(actDescriptionChanged);
            await showAddedHandler.Handle(showAdded);

            repository.Shows.Single().ActDescription.Title.Should().Be("Modified Title");
        }

        [Fact]
        public async Task WhenVenueDescriptionIsChangedAfterShowIsAdded_ThenShowIsUpdated()
        {
            var showAdded = GivenShowAdded(venueName: "Original Name", venueDescriptionAge: 1);
            var venueDescriptionChanged = GivenVenueDescriptionChanged(venueName: "Modified Name");

            await showAddedHandler.Handle(showAdded);
            await venueDescriptionChangedHandler.Handle(venueDescriptionChanged);

            repository.Shows.Single().VenueDescription.Name.Should().Be("Modified Name");
        }

        [Fact]
        public async Task WhenVenueDescriptionChangeArrivesBeforeAfterShowAdded_ThenShowUsesLatestDescription()
        {
            var showAdded = GivenShowAdded(venueName: "Original Name", venueDescriptionAge: 1);
            var venueDescriptionChanged = GivenVenueDescriptionChanged(venueName: "Modified Name");

            await venueDescriptionChangedHandler.Handle(venueDescriptionChanged);
            await showAddedHandler.Handle(showAdded);

            repository.Shows.Single().VenueDescription.Name.Should().Be("Modified Name");
        }

        private ShowAdded GivenShowAdded(
            string actTitle = "New Act",
            int actDescriptionAge = 0,
            string venueName = "New Venue",
            int venueDescriptionAge = 0)
        {
            return new ShowAdded
            {
                act = new ActRepresentation
                {
                    actGuid = actGuid,
                    description = GivenActDescription(actTitle, actDescriptionAge)
                },
                venue = new VenueRepresentation
                {
                    venueGuid = venueGuid,
                    description = GivenVenueDescription(venueName, venueDescriptionAge),
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
                },
                show = new ShowRepresentation
                {
                    startTime = DateTimeOffset.Now
                }
            };
        }

        private ActDescriptionChanged GivenActDescriptionChanged(
            string actTitle = "New Act",
            int actDescriptionAge = 0)
        {
            return new ActDescriptionChanged
            {
                actGuid = actGuid,
                description = GivenActDescription(actTitle, actDescriptionAge)
            };
        }

        private static ActDescriptionRepresentation GivenActDescription(
            string actTitle = "New Act",
            int actDescriptionAge = 0)
        {
            return new ActDescriptionRepresentation
            {
                title = actTitle,
                imageHash = "abc123",
                modifiedDate = DateTime.UtcNow.AddDays(-actDescriptionAge)
            };
        }

        private VenueDescriptionChanged GivenVenueDescriptionChanged(
            string venueName = "New Venue",
            int venueDescriptionAge = 0)
        {
            return new VenueDescriptionChanged
            {
                venueGuid = venueGuid,
                description = GivenVenueDescription(venueName, venueDescriptionAge)
            };
        }

        private static VenueDescriptionRepresentation GivenVenueDescription(
            string venueName,
            int venueDescriptionAge)
        {
            return new VenueDescriptionRepresentation
            {
                name = venueName,
                city = "Anytown, VA",
                modifiedDate = DateTime.UtcNow.AddDays(-venueDescriptionAge)
            };
        }
    }
}
