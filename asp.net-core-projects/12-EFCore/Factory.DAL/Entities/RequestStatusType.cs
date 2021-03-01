using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.DAL.Entities
{
    public class RequestStatusType
    {
        [Column("RequestStatusCode")]
        public int RequestStatusTypeId { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string RequestStatusValue { get; set; }

        public List<Request> Requests { get; set; }
    }
}
