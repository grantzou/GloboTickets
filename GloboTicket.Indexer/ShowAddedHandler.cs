using GloboTicket.Promotion.Messages.Shows;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    class ShowAddedHandler
    {
        private readonly IRepository repository;

        public ShowAddedHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(ShowAdded message)
        {
            Console.WriteLine($"Indexing a show for {message.act.description.title} at {message.venue.description.name}.");
            try
            {
                await repository.IndexShow(message);
                Console.WriteLine("Succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}