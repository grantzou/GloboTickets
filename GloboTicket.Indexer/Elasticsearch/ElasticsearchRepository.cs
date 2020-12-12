using Elasticsearch.Net;
using GloboTicket.Indexer.Documents;
using Nest;
using System;
using System.Linq;
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
            var response = await elasticClient.SearchAsync<ActDocument>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(act => act.actGuid)
                        .Query(actGuid)
                    )
                )
            );

            return response.Documents.SingleOrDefault();
        }

        public Task IndexAct(ActDocument act)
        {
            throw new NotImplementedException();
        }

        public async Task IndexShow(ShowDocument message)
        {
            var response = await elasticClient.IndexDocumentAsync(message);
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
                        .Field(f => f.actGuid)
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
    }
}
