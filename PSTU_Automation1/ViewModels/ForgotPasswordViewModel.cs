using System.ComponentModel.DataAnnotations;

namespace PSTU_Automation1.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
