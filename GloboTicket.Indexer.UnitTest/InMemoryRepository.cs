using GloboTicket.Promotion.Messages.Acts;
using GloboTicket.Promotion.Messages.Shows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GloboTicket.Indexer.UnitTest
{
    class InMemoryRepository : IRepository
    {
        private IList<ShowAdded> shows = new List<ShowAdded>();
        private IList<ActRepresentation> acts = new List<ActRepresentation>();

        public ICollection<ShowAdded> Shows => shows;

        public Task<ActRepresentation> GetAct(Guid actGuid)
        {
            return Task.FromResult(acts.SingleOrDefault(act => act.actGuid == actGuid));
        }

        public Task IndexAct(ActRepresentation act)
        {
            acts.Add(DeepCopy(act));
            return Task.CompletedTask;
        }

        public Task IndexShow(ShowAdded showAdded)
        {
            shows.Add(DeepCopy(showAdded));
            return Task.CompletedTask;
        }

        public Task UpdateShowsWithActDescription(Guid actGuid, ActDescriptionRepresentation description)
        {
            foreach (var show in shows.Where(s => s.act.actGuid == actGuid))
            {
                show.act.description = DeepCopy(description);
            }
            return Task.CompletedTask;
        }

        private static T DeepCopy<T>(T obj)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(obj));
        }
    }
}