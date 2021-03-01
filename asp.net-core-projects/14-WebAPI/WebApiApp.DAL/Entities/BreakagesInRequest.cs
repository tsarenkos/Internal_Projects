using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiApp.DAL.Entities
{
    public partial class BreakagesInRequest
    {
        public int BreakageId { get; set; }
        public int RequestId { get; set; }

        public virtual Breakage Breakage { get; set; }
        public virtual Request Request { get; set; }
    }
}
