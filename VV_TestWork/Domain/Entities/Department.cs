using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VV_TestWork.Domain.Entities
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public Guid SuperVisorId { get; set; }

        public virtual ICollection<Supervisor> SuperVisors { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
