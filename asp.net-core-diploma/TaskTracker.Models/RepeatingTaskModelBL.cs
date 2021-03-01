
namespace TaskTracker.Models
{
    public class RepeatingTaskModelBL
    {
        public int Id { get; set; }
        public int PeriodCode { get; set; }
        public int Multiplier { get; set; }
        public string PeriodName { get; set; }      
    }
}
