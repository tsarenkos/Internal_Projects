using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.DAL.Entities
{
    public class Breakage
    {        
        public int BreakageId { get; set; }
        public int MachineId { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string BreakageName { get; set; }
        public string Description { get; set; }

        [Column(TypeName ="date")]
        public DateTime DateOfCreation { get; set; }
        public int EmployeeId { get; set; }
        public int ShiftNumber { get; set; }
        public int CriticalLevelId { get; set; }
        public int? RequestId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [ForeignKey("MachineId")]
        public Machine Machine { get; set; }

        [ForeignKey("CriticalLevelId")]
        public CriticalLevelType CriticalLevelType { get; set; }

        [ForeignKey("RequestId")]
        public Request Request { get; set; }

    }
}
