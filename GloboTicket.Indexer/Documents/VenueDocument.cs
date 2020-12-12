namespace GloboTicket.Indexer.Documents
{
    public class VenueDocument
    {
        // Hash of the alternate key
        public string _id { get; set; }

        // Alternate key
        public string venueGuid { get; set; }

        // Content
        public VenueDescription description { get; set; }
        public VenueLocation location { get; set; }
    }
}
