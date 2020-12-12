using GloboTicket.Promotion.Messages.Acts;
using GloboTicket.Promotion.Messages.Shows;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    public class ShowAddedHandler
    {
        private readonly IRepository repository;
        private readonly ActUpdater actUpdater;

        public ShowAddedHandler(IRepository repository, ActUpdater actUpdater)
        {
            this.repository = repository;
            this.actUpdater = actUpdater;
        }

        public async Task Handle(ShowAdded showAdded)
        {
            Console.WriteLine($"Indexing a show for {showAdded.act.description.title} at {showAdded.venue.description.name}.");
            try
            {
                ActRepresentation act = await actUpdater.UpdateAndGetLatestAct(showAdded.act.actGuid, showAdded.act.description);
                showAdded.act = act;
                await repository.IndexShow(showAdded);
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