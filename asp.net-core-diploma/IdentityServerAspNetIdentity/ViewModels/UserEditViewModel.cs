using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityServerAspNetIdentity.ViewModels
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Не задан NickName.")]
        [Display(Name = "Имя пользователя*:")]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "не заполнена фамилия")]
        [MaxLength(250)]
        [DisplayName("Фамилия*:")]
        public string FamilyName { get; set; }

        [Required(ErrorMessage = "не заполнено имя")]
        [MaxLength(250)]
        [DisplayName("Имя*:")]
        public string Name { get; set; }

        [MaxLength(250)]
        [DisplayName("Отчество:")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Не задан Email.")]
        [MaxLength(256)]
        [Display(Name = "Электронная почта*:")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не задан Телефон.")]
        [MaxLength(256)]
        public string Telephone { get; set; }
    }
}
