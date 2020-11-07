using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTickets.Promotion.DataAccess.Entities;
using GloboTickets.Promotion.Models;
using Microsoft.EntityFrameworkCore;

namespace GloboTickets.Promotion.DataAccess
{
    public class ShowQueries
    {
        private readonly PromotionContext repository;

        public ShowQueries(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task<List<ShowModel>> ListShows()
        {
            var result = await repository.Show
                .Where(show => !show.Removed.Any())
                .Select(show => new
                {
                    Show = show,
                    Description = show.Descriptions
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return result
                .Select(row => new ShowModel
                {
                    ShowGuid = row.Show.ShowGuid,
                    Description = MapShowDescription(row.Description)
                })
                .ToList();
        }

        public async Task<ShowModel> GetShow(Guid showGuid)
        {
            var result = await repository.Show
                .Where(show => show.ShowGuid == showGuid)
                .Select(show => new
                {
                    Show = show,
                    Description = show.Descriptions
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .SingleOrDefaultAsync();

            return result == null ? null : new ShowModel
            {
                ShowGuid = result.Show.ShowGuid,
                Description = MapShowDescription(result.Description)
            };
        }

        private static ShowDescriptionModel MapShowDescription(ShowDescription showDescription)
        {
            return showDescription == null ? null : new ShowDescriptionModel
            {
                Title = showDescription.Title,
                Date = showDescription.Date,
                City = showDescription.City,
                Venue = showDescription.Venue,
                ImageHash = showDescription.ImageHash,
                LastModifiedTicks = showDescription.ModifiedDate.Ticks
            };
        }
    }
}