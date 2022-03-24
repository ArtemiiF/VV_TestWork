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

namespace VV_TestWork.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для EditEntityView.xaml
    /// </summary>
    public partial class EditEntityView : UserControl
    {
        public EditEntityView()
        {
            InitializeComponent();
            GenderComboBox.ItemsSource = Enum.GetValues(typeof(Core.Gender));
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsGood);
        }

        bool IsGood(char c)
        {
            if (c >= '0' && c <= '9')
                return true;

            return false;
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsGood))
                e.CancelCommand();
        }
    }
}
