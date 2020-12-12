using GloboTicket.Promotion.Messages.Acts;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    public class ActDescriptionChangedHandler
    {
        private readonly IRepository repository;
        private readonly ActUpdater actUpdater;

        public ActDescriptionChangedHandler(IRepository repository, ActUpdater actUpdater)
        {
            this.repository = repository;
            this.actUpdater = actUpdater;
        }

        public async Task Handle(ActDescriptionChanged actDescriptionChanged)
        {
            Console.WriteLine($"Updating index for act {actDescriptionChanged.description.title}.");
            try
            {
                ActRepresentation act = await actUpdater.UpdateAndGetLatestAct(actDescriptionChanged.actGuid, actDescriptionChanged.description);
                await repository.UpdateShowsWithActDescription(act.actGuid, act.description);
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
