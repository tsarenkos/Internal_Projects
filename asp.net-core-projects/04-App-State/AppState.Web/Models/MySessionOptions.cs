namespace AppState.Web.Models
{
    public class MySessionOptions
    {
        public const string SECTION = "CookiesConfig";
        public string Name { get; set; }
        public double IdleTimeout { get; set; }
    }
}
