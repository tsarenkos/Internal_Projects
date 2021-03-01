using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiApp.DAL.Entities
{
    public partial class Request
    {
        public Request()
        {
            BreakagesInRequests = new HashSet<BreakagesInRequest>();
            CorrectingRequests = new HashSet<CorrectingRequest>();
            InverseInnerRequest = new HashSet<Request>();
        }

        public int RequestId { get; set; }
        public int RequestCreatorId { get; set; }
        public int? RequestHadlerId { get; set; }
        public DateTime DateOfCreate { get; set; }
        public int MachineId { get; set; }
        public int RequestStatusId { get; set; }
        public int? InnerRequestId { get; set; }

        public virtual Request InnerRequest { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual Employee RequestCreator { get; set; }
        public virtual Employee RequestHadler { get; set; }
        public virtual RequestStatusType RequestStatus { get; set; }
        public virtual CancelledRequest CancelledRequest { get; set; }
        public virtual ICollection<BreakagesInRequest> BreakagesInRequests { get; set; }
        public virtual ICollection<CorrectingRequest> CorrectingRequests { get; set; }
        public virtual ICollection<Request> InverseInnerRequest { get; set; }
    }
}
