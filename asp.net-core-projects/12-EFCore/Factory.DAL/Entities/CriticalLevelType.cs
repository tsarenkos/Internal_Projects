using System;
using System.Collections.Generic;


namespace Factory.DAL.Entities
{
    public class CriticalLevelType
    {
        public int CriticalLevelTypeId { get; set; }
        public string CriticalLevelValue { get; set; }

        public List<Breakage> Breakages { get; set; }
    }
}
