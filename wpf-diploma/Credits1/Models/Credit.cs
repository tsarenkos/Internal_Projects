using System;
using System.Collections.Generic;

namespace Credits1.Models
{
    public partial class Credit
    {
        public Credit()
        {
            this.Collaterals = new List<Collateral>();
            this.Interest_rate = new List<Interest_rate>();            
            this.Overdue_interest = new List<Overdue_interest>();
            this.Overdue_principal_debt = new List<Overdue_principal_debt>();
            this.Principal_balance = new List<Principal_balance>();
            this.FineOnCredits = new List<FineOnCredit>();
            this.FineOnInterests = new List<FineOnInterest>();
        }

        public string Credit_agreement { get; set; }
        public System.DateTime Start_date { get; set; }
        public System.DateTime End_date { get; set; }
        public int Currency_Id { get; set; }
        public int Sum { get; set; }
        public string Id_Firm { get; set; }
        public Nullable<System.DateTime> Closing_date { get; set; }
        public virtual Accrued_interest Accrued_interest { get; set; }
        public virtual ICollection<Collateral> Collaterals { get; set; }
        public virtual Currencies_directory Currencies_directory { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual ICollection<Interest_rate> Interest_rate { get; set; }        
        public virtual ICollection<Overdue_interest> Overdue_interest { get; set; }
        public virtual ICollection<Overdue_principal_debt> Overdue_principal_debt { get; set; }
        public virtual ICollection<Principal_balance> Principal_balance { get; set; }
        public virtual ICollection<FineOnCredit> FineOnCredits { get; set; }
        public virtual ICollection<FineOnInterest> FineOnInterests { get; set; }
        
    }
}
