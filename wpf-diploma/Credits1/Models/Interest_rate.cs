namespace Credits1.Models
{
    public partial class Interest_rate
    {
        public string Credit_agreement { get; set; }
        public System.DateTime Date { get; set; }
        public double Rate { get; set; }
        public virtual Credit Credit { get; set; }
    }
}
