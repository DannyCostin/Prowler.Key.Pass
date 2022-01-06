using Prowler.KeyPass.Presentation.Helper;
using Prowler.KeyPass.Presentation.Model;
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

namespace Prowler.KeyPass.Presentation.View
{
    /// <summary>
    /// Interaction logic for KeyPassGroupOperation.xaml
    /// </summary>
    public partial class KeyPassGroupOperation : UserControl
    {
        public KeyPassFileModel Model
        {
            get { return (KeyPassFileModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(KeyPassFileModel), typeof(KeyPassGroupOperation));

        public KeyPassGroupOperation()
        {
            InitializeComponent();
        }

        private void btnOperation_Click(object sender, RoutedEventArgs e)
        {
            if (Model?.GroupOperationView == null)
            {
                return;
            }

            if (Model.GroupOperationView.IsNewEntry
                && Model.NewGroup())
            {
                Model.GroupOperationView.ShowGroupOperationView = Visibility.Hidden;
                Model.ShowSaveControl = Visibility.Visible;
            }

            if(!Model.GroupOperationView.IsNewEntry
               && Model.EditGroup())
            {
                Model.GroupOperationView.ShowGroupOperationView = Visibility.Hidden;
                Model.ShowSaveControl = Visibility.Visible;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IconList.ItemsSource = IconHelper.IconResource;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (Model?.GroupOperationView == null)
            {
                return;
            }

            Model.GroupOperationView.ShowGroupOperationView = Visibility.Hidden;
            Model.CancelGroupOperation();
        }
    }
}
