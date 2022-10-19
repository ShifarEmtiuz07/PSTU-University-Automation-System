using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PSTU_Automation1.ViewModels
{
    public class UserClaimViewModel
    {
        public string ClaimType { get; set; }
        public string Value { get; set; }
        public IList<SelectListItem> Options { get; set; }

    }
}