using System.Collections.Generic;

namespace PSTU_Automation1.ViewModels
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Claims = new List<UserClaimViewModel>();
        }

        public string UserId { get; set; }
        public List<UserClaimViewModel> Claims { get; set; }
    }
}
