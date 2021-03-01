using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskTracker.Storage.Core.Entities
{
    public class UserInTaskType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }  //owner, member

        public List<UsersInTask> UsersInTasks { get; set; }
    }
}
