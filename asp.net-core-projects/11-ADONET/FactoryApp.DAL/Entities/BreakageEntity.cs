using System;

namespace FactoryApp.DAL.Entities
{
    public class BreakageEntity
    {
        public int BreakageId { get; set; }
        public string BreakageName { get; set; }
        public string Description { get; set; }
        public int MachineId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int EmployeeId { get; set; }
        public int ShiftNumber { get; set; }
        public int CriticalLevelId { get; set; }
    }
}
