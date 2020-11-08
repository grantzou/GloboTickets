using System;

namespace GloboTickets.Promotion.Models
{
    public class VenueModel
    {
        public Guid VenueGuid { get; set; }
        public VenueDetailsModel Details { get; set; }
    }
}