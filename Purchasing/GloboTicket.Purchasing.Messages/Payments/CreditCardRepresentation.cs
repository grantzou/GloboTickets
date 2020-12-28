﻿namespace GloboTicket.Purchasing.Messages.Payments
{
    public class CreditCardRepresentation
    {
        public string creditCardNumber { get; set; }
        public string name { get; set; }
        public int csv { get; set; }
        public int expirationMonth { get; set; }
        public int expirationYear { get; set; }
    }
}
