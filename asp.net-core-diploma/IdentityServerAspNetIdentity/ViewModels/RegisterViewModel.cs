using System.ComponentModel.DataAnnotations;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не задан NickName.")]
        [Display(Name = "Имя пользователя*:")]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Display(Name = "Пароль*:")]
        [Required(ErrorMessage = "Не задан пароль.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Подтвердите пароль:")]
        [Compare("Password",ErrorMessage ="Пароль и подтверждение пароля не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не задан Email.")]
        [MaxLength(256)]
        [Display(Name = "Электронная почта*:")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
