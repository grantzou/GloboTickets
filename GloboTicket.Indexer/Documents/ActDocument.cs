namespace GloboTicket.Indexer.Documents
{
    public class ActDocument
    {
        // Hash of the alternate key
        public string _id { get; set; }

        // Alternate key
        public string actGuid { get; set; }

        // Content
        public ActDescription description { get; set; }
    }
}
