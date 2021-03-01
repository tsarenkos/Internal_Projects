using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.Storage.Core.Entities
{    public class Deliverer
    {
        public int DelivererId { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string DelivererName { get; set; }

        public List<Machine> Machines { get; set; }
    }
}
