using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VV_TestWork.Domain.Entities;

namespace VV_TestWork.Domain
{
    public class AppDbContext : DbContext, IDbRepository
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Связь подразделения с сотрудниками один ко многим
            builder.Entity<Employee>().HasOne<Department>(e => e.Department).WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId).IsRequired(false);

            //Связб подразделения с начальником один к одному
            builder.Entity<Supervisor>().HasKey(e=> new { e.SupervisorId, e.DepartmentId });
            builder.Entity<Supervisor>().HasOne<Employee>(e => e.Employee).WithMany(p => p.Supervisors);
            builder.Entity<Supervisor>().HasOne<Department>(e=>e.Department).WithMany(p=>p.SuperVisors);
            //Связь сотрудника с заказами один к многим
            builder.Entity<Order>().HasOne<Employee>(e => e.Employee).WithMany(d=>d.Orders).HasForeignKey(e => e.EmployeeId).IsRequired(false);
            


            builder.Entity<Employee>().HasData(new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Валерий",
                Surname = "Жмышенко",
                Gender = Core.Gender.Male,
                MiddleName = "Альбертович",
            });

            builder.Entity<Department>().HasData(new Department
            {
                Id = Guid.NewGuid(),
                Name = "База"
            });


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost\\sqlexpress;database=vv_testwork;trusted_connection=true");
            base.OnConfiguring(optionsBuilder);
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
            SaveChanges();
        }
        public void EditEmployee(Employee employee)
        {
            var local = Set<Employee>().Local.FirstOrDefault(entry => entry.Id.Equals(employee.Id));
             
            if (local != null)
                Entry(local).State = EntityState.Detached;
            
            Entry(employee).State = EntityState.Modified;

            SaveChanges();    
        }

        public void AddDepartment(Department department, Employee supervisor)
        {
            Supervisor sv = new Supervisor();
            
            if (supervisor is not null)
            {
                sv = new Supervisor()
                {
                    Id = Guid.NewGuid(),
                    Department = department,
                    Employee = supervisor
                };

                Supervisors.Add(sv);
                

                department.SuperVisors.Add(sv);
                department.SuperVisorId = sv.Id;
            }

            Departments.Add(department);
            SaveChanges();
        }

        public void EditDepartment(Department department)
        {
            var local = Set<Department>().Local.FirstOrDefault(entry => entry.Id.Equals(department.Id));

            if (local != null)
                Entry(local).State = EntityState.Detached;

            Entry(department).State = EntityState.Modified;

            SaveChanges();
        }

        public void AddOrder(Order order)
        {
            Orders.Add(order);
            this.SaveChanges();
        }

        public void EditOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrdersById(int orderId)
        {
            List<Order> tempOrders = this.Orders.Where(o => o.OrderId == orderId).ToList();

            foreach (var item in tempOrders)
            {
                Orders.Remove(item);
            }
            
        }
    }
}
