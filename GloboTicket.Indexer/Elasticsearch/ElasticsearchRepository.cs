using Elasticsearch.Net;
using GloboTicket.Promotion.Messages.Acts;
using GloboTicket.Promotion.Messages.Shows;
using Nest;
using System;
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

        public Task<ActRepresentation> GetAct(Guid actGuid)
        {
            throw new NotImplementedException();
        }

        public Task IndexAct(ActRepresentation act)
        {
            throw new NotImplementedException();
        }

        public async Task IndexShow(ShowAdded message)
        {
            var response = await elasticClient.IndexDocumentAsync(message);
            if (!response.IsValid)
            {
                throw new InvalidOperationException($"Error indexing show: {response.DebugInformation}");
            }
        }

        public async Task UpdateShowsWithActDescription(Guid actGuid, ActDescriptionRepresentation description)
        {
            await elasticClient.UpdateByQueryAsync<ShowAdded>(ubq => ubq
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.act.actGuid)
                        .Query(actGuid.ToString())
                    )
                )
                .Script(s => s
                    .Source("ctx._source.act.description = params.description")
                    .Params(p => p
                        .Add("description", description)
                    )
                )
                .Conflicts(Conflicts.Proceed)
                .Refresh(true)
            );
        }
    }
}
