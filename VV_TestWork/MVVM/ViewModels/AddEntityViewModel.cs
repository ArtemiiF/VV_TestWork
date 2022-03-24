using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VV_TestWork.Core;
using VV_TestWork.Domain;
using VV_TestWork.Domain.Entities;

namespace VV_TestWork.MVVM.ViewModels
{
    internal class AddEntityViewModel : BaseViewModel
    {
        private string employeeName;
        private string employeeSurname;
        private string employeeMiddlename;
        private DateTime employeeBirthDate = DateTime.Now.AddDays(-1);
        private Gender employeeGender;
        private Domain.Entities.Department employeeDepartment;
        private string departmentName;
        private Domain.Entities.Employee selectedDepartmentSupervisor;
        private Domain.Entities.Employee selectedOrderEmployee;
        private int orderNumber;
        private string orderProducts;
        private IDbRepository appDbContext;

        //Employee fields
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
                OnPropertyChanged(nameof(EmployeeBirthDate));
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
            get => employeeDepartment;
            set
            {
                employeeDepartment = value;
                OnPropertyChanged(nameof(EmployeeSelectedDepartment));
            }
        }
        public RelayCommand AddEmployeeCommand { get; private set; }
        public ObservableCollection<Department> EmployeeDepartmens
        {
            get => employeeDepartmens;
            set
            { 
                employeeDepartmens = value;
                OnPropertyChanged(nameof(EmployeeDepartmens));
            }
        }
        private ObservableCollection<Department> employeeDepartmens;

        //Department fields
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
            get => selectedDepartmentSupervisor;
            set
            {
                selectedDepartmentSupervisor = value;
                OnPropertyChanged(nameof(DepartmentSelectedSupervisor));
            }
        }
        public RelayCommand AddDepartmentCommand { get; private set; }
        public ObservableCollection<Employee> DepartmentSupervisor { get => departmentSupervisor;
            set
            {
                departmentSupervisor = value;
                OnPropertyChanged(nameof(DepartmentSupervisor));
            }
        }
        private ObservableCollection<Employee> departmentSupervisor;

        //Order fields
        public Employee OrderSelectedEmployee
        {
            get => selectedOrderEmployee;
            set
            {
                selectedOrderEmployee = value;
                OnPropertyChanged(nameof(OrderSelectedEmployee));
            }
        }
        public int OrderNumber
        {
            get => orderNumber;
            set
            {
                orderNumber = value;
                OnPropertyChanged(nameof(orderNumber));
            }
        }
        public string OrderProducts
        {
            get => orderProducts;
            set
            {
                orderProducts = value;
                OnPropertyChanged(nameof(orderProducts));
            }
        }
        public RelayCommand AddOrderCommand { get; private set; }

        public AddEntityViewModel()
        {
            AddEmployeeCommand = new RelayCommand(o => AddEmployee());
            AddDepartmentCommand = new RelayCommand(o => AddDepartment());
            AddOrderCommand = new RelayCommand(o => AddOrder());
        }
        public AddEntityViewModel(IDbRepository context) : this()
        {
            appDbContext = context;
            EmployeeDepartmens = new ObservableCollection<Department>(appDbContext.Departments);
            DepartmentSupervisor = new ObservableCollection<Employee>(appDbContext.Employees);
        }

        private void AddEmployee()
        {
            Employee emp = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = EmployeeName,
                Surname = EmployeeSurname,
                MiddleName = EmployeeMiddlename,
                BirthDate = EmployeeBirthDate,
                Gender = EmployeeSelectedGender,
                Department = EmployeeSelectedDepartment
            };

            appDbContext.AddEmployee(emp);

            EmployeeDepartmens = new ObservableCollection<Department>(appDbContext.Departments.ToList());
            DepartmentSupervisor = new ObservableCollection<Employee>(appDbContext.Employees.ToList());
            OnPropertyChanged(nameof(EmployeeDepartmens));
            OnPropertyChanged(nameof(DepartmentSupervisor));
        }

        private void AddDepartment()
        {
            Department d = new Department()
            {
                Id= Guid.NewGuid(),
                Name = DepartmentName,
            };

            appDbContext.AddDepartment(d, DepartmentSelectedSupervisor);

            EmployeeDepartmens = new ObservableCollection<Department>(appDbContext.Departments.ToList());
            DepartmentSupervisor = new ObservableCollection<Employee>(appDbContext.Employees.ToList());
            OnPropertyChanged(nameof(EmployeeDepartmens));
            OnPropertyChanged(nameof(DepartmentSupervisor));
        }

        private void AddOrder()
        {
            Order o = new Order()
            {
                Employee = OrderSelectedEmployee,
                OrderId = OrderNumber,
            };

            var products = OrderProducts.Trim().Split(",").ToList();

            foreach (var item in products)
            {
                var titem = item.Trim();
                if (titem != null && titem != "")
                {
                    o.Id = Guid.NewGuid();
                    o.Product = titem;
                    appDbContext.AddOrder(o);
                }
  
            }
       
        }

    }
}
