using System;
using System.Collections.Generic;

namespace GloboTickets.Promotion.DataAccess.Entities
{
    public class Venue
    {
        public int VenueId { get; set; }
        public Guid VenueGuid { get; set; }

        public ICollection<VenueDetails> Details { get; set; } = new List<VenueDetails>();
    }
}