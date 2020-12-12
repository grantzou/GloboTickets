using GloboTicket.Promotion.Messages.Acts;
using GloboTicket.Promotion.Messages.Shows;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    public class ShowAddedHandler
    {
        private readonly IRepository repository;

        public ShowAddedHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(ShowAdded showAdded)
        {
            Console.WriteLine($"Indexing a show for {showAdded.act.description.title} at {showAdded.venue.description.name}.");
            try
            {
                ActRepresentation act = await repository.GetAct(showAdded.act.actGuid);
                if (act == null || act.description.modifiedDate < showAdded.act.description.modifiedDate)
                {
                    await repository.IndexAct(showAdded.act);
                }
                else
                {
                    showAdded.act = act;
                }
                await repository.IndexShow(showAdded);
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