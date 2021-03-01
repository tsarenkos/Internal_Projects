using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.DAL.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Position { get; set; }

        public List<Breakage> Breakages { get; set; }
        public List<Request> RequestsCreated  { get; set; }
        public List<Request> RequestsHandled { get; set; }
    }
}
