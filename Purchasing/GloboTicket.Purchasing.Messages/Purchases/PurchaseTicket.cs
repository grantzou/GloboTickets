using GloboTicket.Purchasing.Messages.Offers;
using GloboTicket.Purchasing.Messages.Payments;

namespace GloboTicket.Purchasing.Messages.Purchases
{
    public class PurchaseTicket
    {
        public OfferRepresentation offer { get; set; }
        public CreditCardRepresentation creditCard { get; set; }
    }
}
