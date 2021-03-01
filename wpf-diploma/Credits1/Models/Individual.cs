using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class Individual
    {
        public Individual()
        {
            this.Collaterals = new List<Collateral>();
        }

        public string Name { get; set; }
        public string Id_Person { get; set; }
        public System.DateTime Birthday { get; set; }        
        public string Address { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Collateral> Collaterals { get; set; }
        
    }
}
