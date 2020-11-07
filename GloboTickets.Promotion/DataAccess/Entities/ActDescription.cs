using System;
using System.ComponentModel.DataAnnotations;

namespace GloboTickets.Promotion.DataAccess.Entities
{
    public class ActDescription
    {
        public int ActDescriptionId { get; set; }

        public Act Act { get; set; }
        public int ActId { get; set; }
        public DateTime ModifiedDate { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Venue { get; set; }
        [MaxLength(88)]
        public string ImageHash { get; set; }
    }
}