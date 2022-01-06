using Prowler.KeyPass.Presentation.Model;
using Prowler.KeyPass.Presentation.ViewModel;
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
    /// Interaction logic for KeyPassGroupList.xaml
    /// </summary>
    public partial class KeyPassGroupList : UserControl
    {
        public KeyPassFileModel Model
        {
            get { return (KeyPassFileModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(KeyPassFileModel), typeof(KeyPassGroupList));

        public KeyPassGroupList()
        {
            InitializeComponent();         
        }
 
        private void btnNewGroup_Click(object sender, RoutedEventArgs e)
        {
            if(Model == null)
            {
                return;
            }

            Model.GroupOperationView = new KeyPassGroupListViewModel() { IsNewEntry = true };
            Model.GroupOperationView.ShowGroupOperationView = Visibility.Visible;
        }

        private void btnEditEntry_Click(object sender, RoutedEventArgs e)
        {
            if(Model?.GroupOperationView == null)
            {
                return;
            }

            Model.GroupListSelectedValue = (sender as Button)?.DataContext as KeyPassGroupListViewModel;
            Model.GroupOperationView = Model.GroupListSelectedValue;

            if(Model.GroupOperationView != null)
            {
                Model.GroupOperationView.IsNewEntry = false;
                Model.GroupOperationView.ShowGroupOperationView = Visibility.Visible;
            }  
        }

        private void btnDeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            if (Model?.GroupOperationView == null)
            {
                return;
            }

            Model.GroupListSelectedValue = (sender as Button)?.DataContext as KeyPassGroupListViewModel;
            Model.DeleteGroup();
        }
    }
}
