using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Не задано имя пользователя.")]
        [Display(Name ="Имя пользователя:")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не задан Пароль.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
