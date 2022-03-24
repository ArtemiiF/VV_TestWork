using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VV_TestWork.Core;
using VV_TestWork.Domain;

namespace VV_TestWork.MVVM.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly IDbRepository appDbContext;
        public RelayCommand HomeViewCommand { get; private set; }
        public RelayCommand AddEntityViewCommand { get; private set; }
        public RelayCommand EditEntityViewCommand { get; private set; }

        private readonly HomeViewModel homeViewModel;
        private readonly AddEntityViewModel addEntityViewModel;
        private readonly EditEntityViewModel editEntityViewModel;

        private BaseViewModel currentView;
        public BaseViewModel CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainViewModel(IDbRepository context)
        {
            appDbContext = context;

            HomeViewCommand = new RelayCommand(o => HomeClicked());
            AddEntityViewCommand = new RelayCommand(o => AddEntityClicked());
            EditEntityViewCommand = new RelayCommand(o => EditEntityClicked());

            CurrentView = new HomeViewModel(appDbContext);
        }

        private void HomeClicked()
        {

            CurrentView = new HomeViewModel(appDbContext);

        }
        private void AddEntityClicked()
        {
            CurrentView = new AddEntityViewModel(appDbContext);
        }

        private void EditEntityClicked()
        {
            CurrentView = new EditEntityViewModel(appDbContext);
        }

    }
}
