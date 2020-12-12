﻿using GloboTicket.Indexer.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GloboTicket.Indexer.UnitTest
{
    class InMemoryRepository : IRepository
    {
        private List<ShowDocument> shows = new List<ShowDocument>();
        private List<ActDocument> acts = new List<ActDocument>();

        public ICollection<ShowDocument> Shows => shows;

        public Task<ActDocument> GetAct(string actGuid)
        {
            return Task.FromResult(acts.SingleOrDefault(act => act.actGuid == actGuid));
        }

        public Task IndexAct(ActDocument act)
        {
            acts.RemoveAll(a => a.actGuid == act.actGuid);
            acts.Add(DeepCopy(act));
            return Task.CompletedTask;
        }

        public Task IndexShow(ShowDocument ShowDocument)
        {
            shows.RemoveAll(s =>
                s.actGuid == ShowDocument.actGuid &&
                s.venueGuid == ShowDocument.venueGuid &&
                s.startTime == ShowDocument.startTime);
            shows.Add(DeepCopy(ShowDocument));
            return Task.CompletedTask;
        }

        public Task UpdateShowsWithActDescription(string actGuid, ActDescription actDescription)
        {
            foreach (var show in shows.Where(s => s.actGuid == actGuid))
            {
                show.actDescription = DeepCopy(actDescription);
            }
            return Task.CompletedTask;
        }

        private static T DeepCopy<T>(T obj)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(obj));
        }
    }
}