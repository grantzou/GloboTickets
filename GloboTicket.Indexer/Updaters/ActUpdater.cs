using GloboTicket.Indexer.Documents;
using System.Threading.Tasks;

namespace GloboTicket.Indexer.Updaters
{
    public class ActUpdater
    {
        private readonly IRepository repository;

        public ActUpdater(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ActDocument> UpdateAndGetLatestAct(string actGuid, ActDescription actDescription)
        {
            var act = await repository.GetAct(actGuid);
            if (act == null || act.Description.ModifiedDate < actDescription.ModifiedDate)
            {
                act = new ActDocument
                {
                    ActGuid = actGuid,
                    Description = actDescription
                };
                await repository.IndexAct(act);
            }

            return act;
        }
    }
}