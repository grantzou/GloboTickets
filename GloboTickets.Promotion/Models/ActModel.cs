using System;

namespace GloboTickets.Promotion.Models
{
    public class ActModel
    {
        public Guid ActGuid { get; set; }
        public string Title { get; set; }
        public string ImageHash { get; set; }
        public long LastModifiedTicks { get; set; }
    }
}