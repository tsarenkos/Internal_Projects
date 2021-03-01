namespace Credits1.Models
{
    public partial class Accrued_interest
    {
        public string Credit_agreement { get; set; }
        public int Currency_Id { get; set; }
        public double Sum { get; set; }
        public virtual Credit Credit { get; set; }
        public virtual Currencies_directory Currencies_directory { get; set; }
    }
}
