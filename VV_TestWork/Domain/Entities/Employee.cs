using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VV_TestWork.Core;

namespace VV_TestWork.Domain.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public Guid? DepartmentId { get; set; }

        //Навигация к подразделению
        public virtual Department Department { get; set; }
        //Навигация к заказам
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Supervisor> Supervisors { get; set; }
    }


}
