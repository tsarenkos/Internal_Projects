using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class Firm
    {
        public Firm()
        {
            this.Collaterals = new List<Collateral>();
            this.Credits = new List<Credit>();
        }

        public string Name { get; set; }
        public string Id_Firm { get; set; }
        public System.DateTime Registration_date { get; set; }
        public string Phone { get; set; }
        public string Legal_address { get; set; }
        public virtual ICollection<Collateral> Collaterals { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
        
    }
}
