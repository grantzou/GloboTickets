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

        public async Task IndexShow(ShowAdded message)
        {
            var response = await elasticClient.IndexDocumentAsync(message);
            if (!response.IsValid)
            {
                throw new InvalidOperationException($"Error indexing show: {response.DebugInformation}");
            }
        }
    }
}
