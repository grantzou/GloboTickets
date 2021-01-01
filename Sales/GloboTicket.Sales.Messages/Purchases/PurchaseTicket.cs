using GloboTicket.Sales.Messages.Offers;
using GloboTicket.Sales.Messages.Payments;

namespace GloboTicket.Sales.Messages.Purchases
{
    public class PurchaseTicket
    {
        public OfferRepresentation offer { get; set; }
        public CreditCardRepresentation creditCard { get; set; }
    }
}
