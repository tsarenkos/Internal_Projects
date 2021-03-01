using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.Storage.Core.Entities
{
    public class Request    {

        public int RequestId { get; set; }
        public int RequestCreatorId { get; set; }
        public int? RequestHandlerId { get; set; }

        [Column(TypeName ="date")]
        public DateTime DateOfCreate { get; set; }
        public int MachineId { get; set; }
        public int RequestStatusId { get; set; }
        public int? InnerRequestId { get; set; }

        [ForeignKey("RequestCreatorId")]
        public Employee RequestCreator { get; set; }

        [ForeignKey("RequestHandlerId")]
        public Employee RequestHandler { get; set; }

        [ForeignKey("MachineId")]
        public Machine Machine { get; set; }

        [ForeignKey("RequestStatusId")]
        public RequestStatusType RequestStatusType { get; set; }

        [ForeignKey("InnerRequestId")]
        public Request InnerRequest { get; set; }
        public List<Breakage> Breakages { get; set; }
    }
}
