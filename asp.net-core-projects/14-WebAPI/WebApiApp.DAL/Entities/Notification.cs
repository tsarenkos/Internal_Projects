using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiApp.DAL.Entities
{
    public partial class Notification
    {
        public int BreakageId { get; set; }
        public string Message { get; set; }

        public virtual Breakage Breakage { get; set; }
    }
}
