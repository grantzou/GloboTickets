using Elasticsearch.Net;
using GloboTicket.Indexer.Documents;
using Nest;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GloboTicket.Indexer.Elasticsearch
{
    class ElasticsearchRepository : IRepository
    {
        private readonly ElasticClient elasticClient;

        public ElasticsearchRepository(ElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public async Task<ActDocument> GetAct(string actGuid)
        {
            var id = HashOfKey(new { ActGuid = actGuid });
            var response = await elasticClient.GetAsync<ActDocument>(id);

            return response.Found ? response.Source : null;
        }

        public async Task IndexAct(ActDocument act)
        {
            act.Id = HashOfKey(new { ActGuid = act.ActGuid });
            await elasticClient.IndexDocumentAsync(act);
        }

        public async Task IndexShow(ShowDocument show)
        {
            show.Id = HashOfKey(new { ActGuid = show.ActGuid, StartTime = show.StartTime, VenueGuid = show.VenueGuid });
            var response = await elasticClient.IndexDocumentAsync(show);
            if (!response.IsValid)
            {
                throw new InvalidOperationException($"Error indexing show: {response.DebugInformation}");
            }
        }

        public async Task UpdateShowsWithActDescription(string actGuid, ActDescription description)
        {
            await elasticClient.UpdateByQueryAsync<ShowDocument>(ubq => ubq
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.ActGuid)
                        .Query(actGuid)
                    )
                )
                .Script(s => s
                    .Source("ctx._source.actDescription = params.actDescription")
                    .Params(p => p
                        .Add("actDescription", description)
                    )
                )
                .Conflicts(Conflicts.Proceed)
                .Refresh(true)
            );
        }

        private string HashOfKey(object key)
        {
            string json = JsonSerializer.Serialize(key);
            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(json));
            return Convert.ToBase64String(hash);
        }
    }
}
