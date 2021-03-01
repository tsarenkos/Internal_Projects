using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Models
{
    public class UserFriendBL
    {
        public string UserId { get; set; }

        public string FriendId { get; set; }
        [ForeignKey("FriendId")]
        public UserProfileBL Friend { get; set; }

        [Required]
        public DateTime AddDate { get; set; } //дата добавления в друзья

        [Required]
        public bool IsApproved { get; set; } // запрос подтвержден
    }
}
