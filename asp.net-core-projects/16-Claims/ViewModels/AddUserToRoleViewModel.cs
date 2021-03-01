using System.ComponentModel.DataAnnotations;


namespace Claims_App.ViewModels
{
    public class AddUserToRoleViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleName { get; set; }        
    }
}
