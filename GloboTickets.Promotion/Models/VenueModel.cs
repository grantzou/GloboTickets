using System;

namespace GloboTickets.Promotion.Models
{
    public class VenueModel
    {
        public Guid VenueGuid { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public long LastModifiedTicks { get; set; }
    }
}