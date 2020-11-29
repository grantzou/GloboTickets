using System;
using System.Collections.Generic;
using GloboTickets.Promotion.Acts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GloboTickets.Promotion.Shows
{
    public class CreateShowViewModel
    {
        public ActInfo Act { get; set; }
        public List<SelectListItem> Venues { get; set; }
        public Guid Venue { get; set; }
        public DateTime StartTime { get; set; }
    }
}
