using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiApp.DAL.Entities
{
    public partial class CriticalLevelType
    {
        public CriticalLevelType()
        {
            Breakages = new HashSet<Breakage>();
        }

        public int CriticalLevelId { get; set; }
        public string CriticalLevelValue { get; set; }

        public virtual ICollection<Breakage> Breakages { get; set; }
    }
}
