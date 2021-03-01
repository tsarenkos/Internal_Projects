using System;

namespace FactoryApp.BLL.BusinessModels
{
    public class RequestModel    {
        public int RequestCreatorId { get; set; }
        public int? RequestHandlerId { get; set; }
        public DateTime DateOfCreate { get; set; }
        public int MachineId { get; set; }
        public int RequestStatusId { get; set; }
        public int? InnerRequestId { get; set; }
    }
}
