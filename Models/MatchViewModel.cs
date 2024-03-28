using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ProjectASP.Models
{
    public class MatchViewModel
    {
        public Match Match { get; set; }
        public List<SelectListItem> Rondes { get; set; }

        public MatchViewModel()
        {
            Rondes = new List<SelectListItem>();
        }
    }
}