using GloboTicket.Indexer.Documents;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    public interface IRepository
    {
        Task IndexShow(ShowDocument show);
        Task UpdateShowsWithActDescription(string actGuid, ActDescription actDescription);
        Task<ActDocument> GetAct(string actGuid);
        Task IndexAct(ActDocument act);
    }
}