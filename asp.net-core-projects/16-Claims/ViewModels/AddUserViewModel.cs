using System.ComponentModel.DataAnnotations;


namespace Claims_App.ViewModels
{
    public class AddUserViewModel
    {
        [Required]       
        public string Email { get; set; }

        public string Country { get; set; }

        [Required]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password don't match.")]
        [DataType(DataType.Password)]       
        public string PasswordConfirm { get; set; }
    }
}
