using GloboTicket.Promotion.Messages.Acts;
using System;

namespace GloboTicket.Indexer.Documents
{
    public class ActDescription
    {
        public string title { get; set; }
        public string imageHash { get; set; }
        public DateTime modifiedDate { get; set; }

        public static ActDescription FromRepresentation(ActDescriptionRepresentation description)
        {
            return new ActDescription
            {
                title = description.title,
                imageHash = description.imageHash,
                modifiedDate = description.modifiedDate
            };
        }
    }
}