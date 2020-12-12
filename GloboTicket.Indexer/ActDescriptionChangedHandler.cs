using GloboTicket.Promotion.Messages.Acts;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    public class ActDescriptionChangedHandler
    {
        private readonly IRepository repository;

        public ActDescriptionChangedHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(ActDescriptionChanged actDescriptionChanged)
        {
            Console.WriteLine($"Updating index for act {actDescriptionChanged.description.title}.");
            try
            {
                ActRepresentation act = await repository.GetAct(actDescriptionChanged.actGuid);
                if (act == null || act.description.modifiedDate < actDescriptionChanged.description.modifiedDate)
                {
                    await repository.IndexAct(new ActRepresentation
                    {
                        actGuid = actDescriptionChanged.actGuid,
                        description = actDescriptionChanged.description
                    });
                }
                await repository.UpdateShowsWithActDescription(actDescriptionChanged.actGuid, actDescriptionChanged.description);
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
