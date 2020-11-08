using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTickets.Promotion.DataAccess.Entities;
using GloboTickets.Promotion.Models;
using Microsoft.EntityFrameworkCore;

namespace GloboTickets.Promotion.DataAccess
{
    public class ActQueries
    {
        private readonly PromotionContext repository;

        public ActQueries(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task<List<ActModel>> ListActs()
        {
            var result = await repository.Act
                .Where(act => !act.Removed.Any())
                .Select(act => new
                {
                    Show = act,
                    Description = act.Descriptions
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return result
                .Select(row => new ActModel
                {
                    ActGuid = row.Show.ActGuid,
                    Description = MapActDescription(row.Description)
                })
                .ToList();
        }

        public async Task<ActModel> GetAct(Guid actGuid)
        {
            var result = await repository.Act
                .Where(act => act.ActGuid == actGuid)
                .Select(act => new
                {
                    Show = act,
                    Description = act.Descriptions
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .SingleOrDefaultAsync();

            return result == null ? null : new ActModel
            {
                ActGuid = result.Show.ActGuid,
                Description = MapActDescription(result.Description)
            };
        }

        private static ActDescriptionModel MapActDescription(ActDescription actDescription)
        {
            return actDescription == null ? null : new ActDescriptionModel
            {
                Title = actDescription.Title,
                ImageHash = actDescription.ImageHash,
                LastModifiedTicks = actDescription.ModifiedDate.Ticks
            };
        }
    }
}