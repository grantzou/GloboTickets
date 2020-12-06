using GloboTicket.Promotion.Messages.Shows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloboTicket.Indexer.UnitTest
{
    class InMemoryRepository : IRepository
    {
        private IList<ShowAdded> shows = new List<ShowAdded>();

        public ICollection<ShowAdded> Shows => shows;

        public Task IndexShow(ShowAdded showAdded)
        {
            shows.Add(showAdded);
            return Task.FromResult(0);
        }
    }
}