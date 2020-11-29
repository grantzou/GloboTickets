using System;
using System.Collections.Generic;
using GloboTickets.Promotion.Shows;

namespace GloboTickets.Promotion.Acts
{
    public class ActViewModel
    {
        public ActInfo Act { get; set; }
        public List<ShowInfo> Shows { get; set; }
    }
}
