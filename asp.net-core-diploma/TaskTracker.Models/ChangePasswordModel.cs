using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models
{
    public class ChangePasswordModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите старый пароль")]
        [StringLength(255, ErrorMessage = "Длина пароля не менее 6 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        [StringLength(255, ErrorMessage = "Длина пароля не менее 6 символов", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль повторно")]
        [StringLength(255, ErrorMessage = "Длина пароля не менее 6 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage="Новые пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
