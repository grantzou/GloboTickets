using GloboTicket.Sales.Messages.Purchases;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Sales.Purchasing
{
    class PurchaseTicketHandler
    {
        public PurchaseTicketHandler()
        {
        }

        public async Task Handle(PurchaseTicket message)
        {
            Console.WriteLine($"Handling purchase for {message.offer.unitPrice:C}");
        }
    }
}