using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiApp.DAL.Entities
{
    public partial class RequestStatusType
    {
        public RequestStatusType()
        {
            Requests = new HashSet<Request>();
        }

        public int RequestStatusCode { get; set; }
        public string RequestStatusValue { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
