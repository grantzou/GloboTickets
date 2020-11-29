using System;
using GloboTickets.Promotion.Messages.Acts;
using GloboTickets.Promotion.Messages.Venues;

namespace GloboTickets.Promotion.Messages.Shows
{
    public class ShowAdded
    {
        public ActRepresentation act { get; set; }
        public VenueRepresentation venue { get; set; }
        public ShowRepresentation show { get; set; }
    }
}
