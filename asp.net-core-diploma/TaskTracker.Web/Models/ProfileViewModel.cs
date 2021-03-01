using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Web.Models
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "не заполнен Email")]
        [MaxLength(250)]
        [EmailAddress]
        [DisplayName("Email*:")]
        public string Email { get; set; }

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

        [MaxLength(100)]
        [DisplayName("Телефон:")]
        public string telephone { get; set; }

        public bool HasPhoto { get; set; }
        public bool delPhoto { get; set; }
    }
}
