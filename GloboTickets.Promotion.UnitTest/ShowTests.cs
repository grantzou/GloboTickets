using FluentAssertions;
using GloboTickets.Promotion.DataAccess;
using GloboTickets.Promotion.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GloboTickets.Promotion.UnitTest
{
    public class ShowTests
    {
        [Fact]
        public async Task ShowsInitiallyEmpty()
        {
            List<ShowModel> shows = await showQueries.ListShows();
            shows.Should().BeEmpty();
        }

        [Fact]
        public async Task WhenShowIsScheduled_ShowIsReturned()
        {
            Guid actGuid = Guid.NewGuid();
            Guid venueGuid = Guid.NewGuid();
            DateTimeOffset startTime = new DateTimeOffset(2021, 03, 21, 08, 00, 00, CstOffset);
            await showCommands.ScheduleShow(actGuid, venueGuid, startTime);

            var shows = await showQueries.ListShows();
            shows.Should().Contain(show => show.StartTime == startTime);
        }

        static TimeSpan CstOffset = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time").BaseUtcOffset;

        private ShowQueries showQueries;
        private ShowCommands showCommands;

        public ShowTests()
        {
            var repository = new PromotionContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

            showQueries = new ShowQueries(repository);
            showCommands = new ShowCommands(repository);
        }
    }
}
