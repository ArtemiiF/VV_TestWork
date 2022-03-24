using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV_TestWork.Domain.Entities
{
    public class Supervisor
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Employee")]
        public Guid SupervisorId { get; set; }
        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
