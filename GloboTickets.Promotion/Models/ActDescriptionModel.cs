using System;

namespace GloboTickets.Promotion.Models
{
    public class ActDescriptionModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public string ImageHash { get; set; }
        public long LastModifiedTicks { get; set; }
    }
}