using GloboTickets.Promotion.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace GloboTickets.Promotion
{
    public class ShowCommands
    {
        private PromotionContext repository;

        public ShowCommands(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task ScheduleShow(Guid actGuid, Guid venueGuid, DateTimeOffset startTime)
        {
            await repository.GetOrInsertShow(actGuid, venueGuid, startTime);
            await repository.SaveChangesAsync();
        }
    }
}