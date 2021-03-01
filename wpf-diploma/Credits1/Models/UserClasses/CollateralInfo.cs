namespace Credits1.Models.UserClasses
{
    public class CollateralInfo
    {
        //Collateral
        public string Collateral_agreement { get; set; }
        public System.DateTime Start_date { get; set; }
        public System.DateTime End_date { get; set; }
        public string Credit_agreement { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public Currencies_directory Currency { get; set; }
        public double Sum { get; set; }
        public System.DateTime Closing_date { get; set; }
        public int? FormId { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }

    }
}
