namespace Credits1.Models.UserClasses
{
    public class CollateralRegister
    {        
        public string Collateral_agreement { get; set; }
        public System.DateTime Start_date { get; set; }
        public System.DateTime End_date { get; set; } 
        
        public string Subject { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }

        public int Currency_Id { get; set; }
        public double Price { get; set; }
        
        public string ClientId { get; set; }
        public string ClientName { get; set; }
    }
}
