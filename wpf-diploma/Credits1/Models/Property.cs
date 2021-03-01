using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class Property
    {
        public Property()
        {
            this.Collaterals = new List<Collateral>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Collateral_agreement { get; set; }        
        public string Description { get; set; }
        public int Currency_Id { get; set; }
        public double Price { get; set; }        
        public virtual Currencies_directory Currencies_directory { get; set; }
        public virtual ICollection<Collateral> Collaterals { get; set; }
    }
}
