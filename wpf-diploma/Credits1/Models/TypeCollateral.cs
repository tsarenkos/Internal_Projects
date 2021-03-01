using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class TypeCollateral
    {
        public TypeCollateral()
        {
            this.Collaterals = new List<Collateral>();            
        }

        public int TypeId { get; set; }
        public string Type { get; set; }        
        public virtual ICollection<Collateral> Collaterals { get; set; }        
    }
}

