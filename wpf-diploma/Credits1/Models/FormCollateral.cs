using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class FormCollateral
    {
        public FormCollateral()
        {
            this.Collaterals = new List<Collateral>();
        }

        public int FormId { get; set; }
        public string Form { get; set; }
        public virtual ICollection<Collateral> Collaterals { get; set; }        
    }
}