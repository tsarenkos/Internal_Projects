using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiApp.DAL.Entities
{
    public partial class Breakage
    {
        public int BreakeageId { get; set; }
        public int MachineId { get; set; }
        public string BreakeageName { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int EmployeeId { get; set; }
        public int ShiftNumber { get; set; }
        public int CriticalLevelId { get; set; }

        public virtual CriticalLevelType CriticalLevel { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual BreakagesInRequest BreakagesInRequest { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
