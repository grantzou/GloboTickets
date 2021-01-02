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

        public Task Handle(ConsumeContext<PurchaseTicket> context)
        {
            PurchaseTicket message = context.Message;

            if (message.order.quantity > message.offer.maximumQuantity ||
                message.order.quantity < message.offer.minimumQuantity)
            {
                throw new InvalidOperationException("Order quantity is out of range");
            }

            int quantityRemaining = 3;
            if (message.order.quantity > quantityRemaining)
            {
                context.Publish(new PurchaseTicketFailed
                {
                    failureReason = FailureReasons.SoldOut,
                    offer = message.offer,
                    order = message.order
                });
            }

            var units = message.order.quantity == 1 ? "ticket" : "tickets";
            Console.WriteLine($"Handling purchase for {message.order.quantity} {units}");

            return Task.CompletedTask;
        }
    }
}