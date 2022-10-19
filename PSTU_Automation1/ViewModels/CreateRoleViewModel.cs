using System.ComponentModel.DataAnnotations;

namespace PSTU_Automation1.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
