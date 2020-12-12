using GloboTicket.Indexer.Documents;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
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
            if (act == null || act.description.modifiedDate < actDescription.modifiedDate)
            {
                act = new ActDocument
                {
                    actGuid = actGuid,
                    description = actDescription
                };
                await repository.IndexAct(act);
            }

            return act;
        }
    }
}