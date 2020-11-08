using GloboTickets.Promotion.DataAccess.Entities;
using GloboTickets.Promotion.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GloboTickets.Promotion.DataAccess
{
    public class ActCommands
    {
        private readonly PromotionContext repository;

        public ActCommands(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task AddAct(Guid actGuid)
        {
            await repository.GetOrInsertAct(actGuid);
            await repository.SaveChangesAsync();
        }

        public async Task RemoveAct(Guid actGuid)
        {
            var act = await repository.GetOrInsertAct(actGuid);
            await repository.AddAsync(new ActRemoved
            {
                Act = act,
                RemovedDate = DateTime.UtcNow
            });
            await repository.SaveChangesAsync();
        }

        public async Task SetActDescription(Guid actGuid, ActDescriptionModel actDescriptionModel)
        {
            var act = await repository.GetOrInsertAct(actGuid);
            var lastActDescription = act.Descriptions
                .OrderByDescending(description => description.ModifiedDate)
                .FirstOrDefault();
            var modifiedTicks = ToTicks(lastActDescription?.ModifiedDate);
            if (modifiedTicks != actDescriptionModel.LastModifiedTicks)
            {
                throw new DbUpdateConcurrencyException("A new update has occurred since you loaded the page. Please refresh and try again.");
            }

            if (lastActDescription == null ||
                lastActDescription.Title != actDescriptionModel.Title ||
                lastActDescription.ImageHash != actDescriptionModel.ImageHash)
            {
                await repository.AddAsync(new ActDescription
                {
                    ModifiedDate = DateTime.UtcNow,
                    Act = act,
                    Title = actDescriptionModel.Title,
                    ImageHash = actDescriptionModel.ImageHash
                });
                await repository.SaveChangesAsync();
            }
        }

        private long ToTicks(DateTime? optionalDate)
        {
            switch (optionalDate)
            {
                case DateTime date:
                    return date.Ticks;
                case null:
                    return 0;
            }
        }
    }
}