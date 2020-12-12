using GloboTicket.Promotion.Messages.Acts;
using GloboTicket.Promotion.Messages.Shows;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    public interface IRepository
    {
        Task IndexShow(ShowAdded showAdded);
        Task UpdateShowsWithActDescription(Guid actGuid, ActDescriptionRepresentation description);
        Task<ActRepresentation> GetAct(Guid actGuid);
        Task IndexAct(ActRepresentation act);
    }
}