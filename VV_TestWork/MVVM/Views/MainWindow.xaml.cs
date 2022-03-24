using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VV_TestWork.Domain;
using VV_TestWork.MVVM.ViewModels;

namespace VV_TestWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow(AppDbContext context)
        {
            InitializeComponent();
            
            DataContext = new MainViewModel(context);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Вы уверены, что бросили?",
                                               "application",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question,
                                                MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                
                System.Environment.Exit(0);
            }
        }

        private void Tray_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
