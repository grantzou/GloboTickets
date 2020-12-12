using System;

namespace GloboTicket.Indexer.Documents
{
    public class ShowDocument
    {
        // Hash of the alternate key
        public string _id { get; set; }

        // Alternate key
        public string actGuid { get; set; }
        public string venueGuid { get; set; }
        public DateTimeOffset startTime { get; set; }

        // Content
        public ActDescription actDescription { get; set; }
        public VenueDescription venueDescription { get; set; }
        public VenueLocation venueLocation { get; set; }
    }
}
