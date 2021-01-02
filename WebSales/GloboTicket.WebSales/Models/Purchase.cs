using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.WebSales.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }
        [Display(Name = "Name on Card")]
        public string NameOnCard { get; set; }
        [Display(Name = "Verification Code")]
        public string VerificationCode { get; set; }
        [Display(Name = "Expiration Month")]
        public int ExpirationMonth { get; set; }
        [Display(Name = "Expiration Year")]
        public int ExpirationYear { get; set; }
    }
}
