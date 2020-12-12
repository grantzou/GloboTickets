using GloboTicket.Indexer.Documents;
using GloboTicket.Promotion.Messages.Shows;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer.Handlers
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
                string actGuid = showAdded.act.actGuid.ToString();
                ActDescription actDescription = ActDescription.FromRepresentation(showAdded.act.description);
                ActDocument act = await actUpdater.UpdateAndGetLatestAct(actGuid, actDescription);
                var show = new ShowDocument
                {
                    actGuid = act.actGuid,
                    actDescription = act.description
                };
                await repository.IndexShow(show);
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