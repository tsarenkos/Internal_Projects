namespace TaskTracker.Models
{
    public class UserProfileEditBL
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FamilyName { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Telephone { get; set; }

        public string Photo { get; set; }
        public bool delPhoto { get; set; }
        public UserPhotoBL PhotoData {get; set; }

    }
}
