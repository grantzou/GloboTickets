using GloboTicket.Promotion.Messages.Shows;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    public interface IRepository
    {
        Task IndexShow(ShowAdded message);
    }
}