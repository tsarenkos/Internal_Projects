namespace Credits1.Models.UserClasses
{
    public class CreditInfo
    {
        //Credit        
        public string Credit_agreement { get; set; }
        public System.DateTime Start_date { get; set; }
        public System.DateTime End_date { get; set; }
        public Currencies_directory Currency { get; set; }
        public int Sum { get; set; }
        public string Id_Firm { get; set; }

        //Interest_rate
        public double Rate { get; set; }
        public double Rate_overdue { get; set; }

        //Firm
        public string FirmName { get; set; }        
    }
}
