using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiApp.DAL.Entities
{
    public partial class CancelledRequest
    {
        public int RequestId { get; set; }
        public int FitterId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Employee Fitter { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual Request Request { get; set; }
    }
}
