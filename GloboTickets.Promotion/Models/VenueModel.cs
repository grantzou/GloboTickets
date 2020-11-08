using System;

namespace GloboTickets.Promotion.Models
{
    public class VenueModel
    {
        public Guid VenueGuid { get; set; }
        public VenueDescriptionModel Description { get; set; }
    }
}