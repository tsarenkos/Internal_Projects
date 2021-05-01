namespace Middleware_App.Web.Models
{
    public class QueryOptions
    {
        public const string Section = "QueryModel";
        public int LengthMax { get; set; }
        public string ContentDenied { get; set; }
        public string LengthErrorMessage { get; set; }
        public string ContentErrorMessage { get; set; }
    }
}
