using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiApp.DAL.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            Breakages = new HashSet<Breakage>();
            CancelledRequestFitters = new HashSet<CancelledRequest>();
            CancelledRequestManagers = new HashSet<CancelledRequest>();
            CorrectingRequests = new HashSet<CorrectingRequest>();
            RequestRequestCreators = new HashSet<Request>();
            RequestRequestHadlers = new HashSet<Request>();
        }

        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public virtual ICollection<Breakage> Breakages { get; set; }
        public virtual ICollection<CancelledRequest> CancelledRequestFitters { get; set; }
        public virtual ICollection<CancelledRequest> CancelledRequestManagers { get; set; }
        public virtual ICollection<CorrectingRequest> CorrectingRequests { get; set; }
        public virtual ICollection<Request> RequestRequestCreators { get; set; }
        public virtual ICollection<Request> RequestRequestHadlers { get; set; }
    }
}
