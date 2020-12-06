using GloboTicket.Promotion.Messages.Shows;
using Nest;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    class ShowAddedHandler
    {
        private readonly ElasticClient elasticClient;

        public ShowAddedHandler(ElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public async Task Handle(ShowAdded message)
        {
            Console.WriteLine($"Indexing a show for {message.act.description.title} at {message.venue.description.name}.");
            var response = await elasticClient.IndexDocumentAsync(message);
            if (response.IsValid)
            {
                Console.WriteLine("Succeeded");
            }
            else
            {
                Console.WriteLine($"Error indexing show: {response.DebugInformation}");
                throw new InvalidOperationException("Indexer failed");
            }
        }
    }
}