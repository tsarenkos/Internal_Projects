using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskTracker.Web.Models
{
    public class TagInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> TagSelected { get; set; }
    }
}
