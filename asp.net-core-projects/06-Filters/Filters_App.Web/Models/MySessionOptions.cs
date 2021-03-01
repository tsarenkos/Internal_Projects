namespace Filters_App.Web.Models
{
    public class MySessionOptions
    {
        public const string Section = "Session";

        public int MaxLengthArg { get; set; }
        public bool CheckIsCreated { get; set; }
        public bool ChangeLengthArg { get; set; }
        public bool ChangeCaseArg { get; set; }
    }
}
