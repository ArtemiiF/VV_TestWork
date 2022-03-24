using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VV_TestWork.Domain;
using VV_TestWork.Domain.Entities;

namespace VV_TestWork.MVVM.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        IDbRepository appDbContext;

        private ObservableCollection<Employee> employees { get; set; }
        private ObservableCollection<Department> departments { get; set; }
        private ObservableCollection<Order> orders { get; set; }


        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
                employees = value;
                OnPropertyChanged(nameof(Employees));
            }

        }
        public ObservableCollection<Department> Departments
        {
            get => departments;
            set
            {
                departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }
        public ObservableCollection<Order> Orders
        {
            get => orders;
            set
            {
                orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public HomeViewModel()
        {

        }
        public HomeViewModel(IDbRepository context)
        {
            appDbContext = context;
            Employees = new ObservableCollection<Employee>(appDbContext.Employees);
            Departments = new ObservableCollection<Department>(appDbContext.Departments);
            Orders = new ObservableCollection<Order>(appDbContext.Orders);
        }
    }
}
