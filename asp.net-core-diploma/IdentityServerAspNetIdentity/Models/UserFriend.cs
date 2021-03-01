using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityServerAspNetIdentity.Models
{
    public class UserFriend
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string FriendId { get; set; }

        [Required]
        public DateTime AddDate { get; set; } //дата добавления в друзья

        [Required]
        public bool IsApproved { get; set; } // запрос подтвержден
    }
}
