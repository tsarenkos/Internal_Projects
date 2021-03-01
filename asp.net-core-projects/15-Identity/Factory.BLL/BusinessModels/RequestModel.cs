using System;

namespace Factory.BLL.BusinessModels
{
    public class RequestModel    
    {
        public int RequestId { get; set; }
        public int RequestCreatorId { get; set; }
        public int? RequestHandlerId { get; set; }
        public DateTime DateOfCreate { get; set; }
        public int MachineId { get; set; }
        public int RequestStatusId { get; set; }
        public int? InnerRequestId { get; set; }
    }
}
