using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VV_TestWork.Core;
using VV_TestWork.Domain;
using VV_TestWork.Domain.Entities;

namespace VV_TestWork.MVVM.ViewModels
{
    internal class EditEntityViewModel : BaseViewModel
    {
        private IDbRepository appDbContext;
        private Employee employeeSelected;
        private ObservableCollection<Employee> employees;
        private ObservableCollection<Department> departmens;
        private string employeeName;
        private string employeeSurname;
        private string employeeMiddlename;
        private DateTime employeeBirthDate;
        private Gender employeeGender;
        private Department employeeSelectedDepartment;
        private Department departmentSelectedDepartment;
        private string departmentName;
        private Employee departmentSupervisor;
        private ObservableCollection<Order> orders;
        private Order orderSelectedOrder;
        private int orderId;
        private string orderProducts;
        private Employee orderSelectedEmployee;

        //Employee fields
        public Employee EmployeeSelected
        {
            get => employeeSelected;
            set
            {
                employeeSelected = value;
                OnPropertyChanged(nameof(EmployeeSelected));
                employeeIsSelected();
            }
        }
        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
                employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }
        public string EmployeeName
        {
            get => employeeName;
            set
            {
                employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }
        public string EmployeeSurname
        {
            get => employeeSurname;
            set
            {
                employeeSurname = value;
                OnPropertyChanged(nameof(EmployeeSurname));
            }
        }
        public string EmployeeMiddlename
        {
            get => employeeMiddlename;
            set
            {
                employeeMiddlename = value;
                OnPropertyChanged(nameof(EmployeeMiddlename));
            }
        }
        public DateTime EmployeeBirthDate
        {
            get => employeeBirthDate;
            set
            {
                employeeBirthDate = value;
                OnPropertyChanged(nameof (EmployeeBirthDate));
            }
        }
        public ObservableCollection<Department> Departmens
        {
            get => departmens;
            set
            {
                departmens = value;
                OnPropertyChanged(nameof(Departmens));
            }
        }
        public Gender EmployeeSelectedGender
        {
            get => employeeGender;
            set
            {
                employeeGender = value;
                OnPropertyChanged(nameof(EmployeeSelectedGender));
            }
        }
        public Department EmployeeSelectedDepartment
        {
            get => employeeSelectedDepartment;
            set
            {
                employeeSelectedDepartment = value;
                OnPropertyChanged(nameof(EmployeeSelectedDepartment));
            }
        }
        public RelayCommand EditEmployeeCommand { get; private set; }

        //Department fields
        public Department DepartmentSelectedDepartment
        {
            get => departmentSelectedDepartment;
            set
            {
                departmentSelectedDepartment = value;
                OnPropertyChanged(nameof(DepartmentSelectedDepartment));
                departmentIsSelected();
            }
        }
        public string DepartmentName
        {
            get => departmentName;
            set
            {
                departmentName = value;
                OnPropertyChanged(nameof(DepartmentName));
            }
        }
        public Employee DepartmentSelectedSupervisor
        {
            get => departmentSupervisor;
            set
            {
                departmentSupervisor = value;
                OnPropertyChanged(nameof(DepartmentSelectedSupervisor));
                
            }
        }
        public RelayCommand EditDepartmentCommand { get; private set; }

        //Order fields
        public ObservableCollection<Order> Orders
        {
            get => orders;
            set
            {
                orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }
        public Order OrderSelectedOrder
        {
            get => orderSelectedOrder;
            set
            {
                orderSelectedOrder = value;
                OnPropertyChanged(nameof(OrderSelectedOrder));
                orderIsSelected();
            }
        }
        public int OrderId
        {
            get => orderId;
            set
            {
                orderId = value;
                OnPropertyChanged(nameof(OrderId));
            }
        }
        public string OrderProducts
        {
            get => orderProducts;
            set
            {
                orderProducts = value;
                OnPropertyChanged(nameof(OrderProducts));
            }
        }
        public Employee OrderSelectedEmployee
        {
            get => orderSelectedEmployee;
            set
            {
                orderSelectedEmployee = value;
                OnPropertyChanged(nameof(OrderSelectedEmployee));
            }
        }


        public RelayCommand EditOrderCommand { get; private set; }  

        public EditEntityViewModel()
        {
            EditEmployeeCommand = new RelayCommand(o => editEmployeeIsClicked());
            EditDepartmentCommand = new RelayCommand(o => editDepartmentIsClicked());
            EditOrderCommand = new RelayCommand(o=> editOrderIsClicked());
        }
        public EditEntityViewModel(IDbRepository context) : this()
        {
            appDbContext = context;
            Employees = new ObservableCollection<Employee>(appDbContext.Employees);
            Departmens = new ObservableCollection<Department>(appDbContext.Departments);

            var tempOrder = context.Orders.GroupBy(l => new { l.OrderId, l.EmployeeId })
                                            .Select(g => new { g.Key.OrderId, g.Key.EmployeeId, Products = string.Join(",", g.Select(i => i.Product)) });

            ObservableCollection<Order> tempOrders = new ObservableCollection<Order>();
            foreach (var item in tempOrder)
            {
                tempOrders.Add(new Order { OrderId = item.OrderId, EmployeeId = item.EmployeeId, Product = item.Products });
            }
            Orders = tempOrders;
        }

        private void employeeIsSelected()
        {
            EmployeeName = EmployeeSelected.Name;
            EmployeeSurname = EmployeeSelected.Surname;
            EmployeeMiddlename = EmployeeSelected.MiddleName;
            EmployeeBirthDate = EmployeeSelected.BirthDate;
            if(EmployeeSelectedDepartment is not null)
                EmployeeSelectedDepartment = EmployeeSelected.Department;

            EmployeeSelectedGender = EmployeeSelected.Gender;

        }
        private void departmentIsSelected()
        {
            DepartmentName = DepartmentSelectedDepartment.Name;
        }

        private void orderIsSelected()
        {
            OrderId = OrderSelectedOrder.OrderId;
            OrderProducts = OrderSelectedOrder.Product;
        }

        private void editEmployeeIsClicked()
        {
            Employee emp = new Employee()
            {
                Name = EmployeeName,
                Surname = EmployeeSurname,
                MiddleName = EmployeeMiddlename,
                BirthDate = EmployeeBirthDate,
                Gender = EmployeeSelectedGender,
                Id = EmployeeSelected.Id
            };
            if (EmployeeSelectedDepartment is not null)
                emp.Department = EmployeeSelectedDepartment;

            appDbContext.EditEmployee(emp);
        }

        private void editDepartmentIsClicked()
        {
            Department d = new Department()
            {
                Id = DepartmentSelectedDepartment.Id,
                Name = DepartmentName,
            };

            if (DepartmentSelectedSupervisor is not null)
            {
                d.SuperVisorId = DepartmentSelectedSupervisor.Id;
            }

            appDbContext.EditDepartment(d);
        }

        private void editOrderIsClicked()
        {
            Order o = new Order()
            {
                OrderId = this.OrderId
            };

            if (OrderSelectedEmployee is not null)
            {
                o.Employee = OrderSelectedEmployee;
            }
            else
            {
                o.EmployeeId = Orders.First(x=>x.OrderId == this.OrderId).EmployeeId;
            }

            var products = OrderProducts.Trim().Split(",").ToList();

            appDbContext.DeleteOrdersById(OrderSelectedOrder.OrderId);

            foreach (var item in products)
            {
                var titem = item.Trim();
                if(titem != null && titem != "")
                {
                    o.Id = Guid.NewGuid();
                    o.Product = titem;
                    appDbContext.AddOrder(o);
                }
                
            }

            
        }
    }
}
