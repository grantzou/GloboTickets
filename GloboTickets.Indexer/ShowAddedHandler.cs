using GloboTickets.Promotion.Messages.Shows;
using System;
using System.Threading.Tasks;

namespace GloboTickets.Indexer
{
    class ShowAddedHandler
    {
        public ShowAddedHandler()
        {
        }

        public Task Handle(ShowAdded message)
        {
            Console.WriteLine($"Added a show for {message.act.description.title} at {message.venue.description.name}.");
            return Task.FromResult(0);
        }
    }
}