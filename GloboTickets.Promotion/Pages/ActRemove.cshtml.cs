using System;
using System.Threading.Tasks;
using GloboTickets.Promotion.Acts;
using GloboTickets.Promotion.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GloboTickets.Promotion.Pages
{
    public class ActRemoveModel : PageModel
    {
        private readonly ActQueries actQueries;

        private readonly ActCommands actCommands;

        public ActRemoveModel(ActQueries actQueries, ActCommands actCommands)
        {
            this.actQueries = actQueries;
            this.actCommands = actCommands;
        }

        [BindProperty(SupportsGet=true)]
        public Guid ActGuid { get; set; }

        public string Title { get; set; }
        public string ImageHash { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var act = await actQueries.GetAct(ActGuid);

            if (act == null)
            {
                return NotFound();
            }
            else
            {
                Title = act.Title;
                ImageHash = act.ImageHash;
                return Page();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            await actCommands.RemoveAct(ActGuid);
            return Redirect("~/");
        }
    }
}
