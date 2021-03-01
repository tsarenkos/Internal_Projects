using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class Currencies_directory
    {
        public Currencies_directory()
        {
            this.Accrued_interest = new List<Accrued_interest>();
            this.Collaterals = new List<Collateral>();
            this.Credits = new List<Credit>();
            this.FineOnCredits = new List<FineOnCredit>();
            this.FineOnInterests = new List<FineOnInterest>();
            this.Overdue_interest = new List<Overdue_interest>();
            this.Overdue_principal_debt = new List<Overdue_principal_debt>();
            this.Principal_balance = new List<Principal_balance>();
            this.Properties = new List<Property>();
        }

        public int Currency_Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Accrued_interest> Accrued_interest { get; set; }
        public virtual ICollection<Collateral> Collaterals { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
        public virtual ICollection<FineOnCredit> FineOnCredits { get; set; }
        public virtual ICollection<FineOnInterest> FineOnInterests { get; set; }
        public virtual ICollection<Overdue_interest> Overdue_interest { get; set; }
        public virtual ICollection<Overdue_principal_debt> Overdue_principal_debt { get; set; }
        public virtual ICollection<Principal_balance> Principal_balance { get; set; }
        public virtual ICollection<Property> Properties { get; set; }

        
    }
}
