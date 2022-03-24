using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VV_TestWork.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public string Product { get; set; } = string.Empty;
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
