using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VV_TestWork.Domain.Entities;

namespace VV_TestWork.Domain
{
    internal interface IDbRepository
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }

        public void AddEmployee(Employee employee);
        public void EditEmployee(Employee employee);

        public void AddDepartment(Department department, Employee supervisor);
        public void EditDepartment(Department department);

        public void AddOrder(Order order);
        public void EditOrder(Order order);
        public void DeleteOrdersById(int orderId);
    }
}
