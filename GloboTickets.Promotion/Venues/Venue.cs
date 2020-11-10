using System;
using System.Collections.Generic;

namespace GloboTickets.Promotion.Venues
{
    public class Venue
    {
        public int VenueId { get; set; }
        public Guid VenueGuid { get; set; }

        public ICollection<VenueDescription> Descriptions { get; set; } = new List<VenueDescription>();
    }
}