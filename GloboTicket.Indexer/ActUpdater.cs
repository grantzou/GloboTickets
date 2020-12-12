using GloboTicket.Promotion.Messages.Acts;
using System;
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

        public async Task<ActRepresentation> UpdateAndGetLatestAct(Guid actGuid, ActDescriptionRepresentation description)
        {
            ActRepresentation act = await repository.GetAct(actGuid);
            if (act == null || act.description.modifiedDate < description.modifiedDate)
            {
                act = new ActRepresentation
                {
                    actGuid = actGuid,
                    description = description
                };
                await repository.IndexAct(act);
            }

            return act;
        }
    }
}