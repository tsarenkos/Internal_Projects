using System;
using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class Collateral
    {
        public Collateral()
        {
            this.Properties = new List<Property>();
        }

        public string Collateral_agreement { get; set; }
        public System.DateTime Start_date { get; set; }
        public System.DateTime End_date { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public int Currency_Id { get; set; }
        public double Sum { get; set; }
        public string Credit_agreement { get; set; }
        public string Id_Person { get; set; }
        public string Id_Firm { get; set; }
        public Nullable<System.DateTime> Closing_date { get; set; }
        public Nullable<int> FormId { get; set; }
        public virtual Monitoring_collateral Monitoring_collateral { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual Credit Credit { get; set; }
        public virtual Currencies_directory Currencies_directory { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual Individual Individual { get; set; }
        public virtual FormCollateral FormCollateral { get; set; }
        public virtual TypeCollateral TypeCollateral { get; set; }
    }
}
