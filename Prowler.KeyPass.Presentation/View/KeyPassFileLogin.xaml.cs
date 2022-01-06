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
    /// Interaction logic for KeyPassFileLogin.xaml
    /// </summary>
    public partial class KeyPassFileLogin : UserControl
    {
        public KeyPassFileModel Model
        {
            get { return (KeyPassFileModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }
        
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(KeyPassFileModel), typeof(KeyPassFileLogin));        

        public KeyPassFileLogin()
        {
            InitializeComponent();           
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Model == null)
            {
                return;
            }

            try
            {
                Model.LoginView.ErrorMessage = string.Empty;
                Model.LoginView.Login = Prowler.KeyPass.Core.ProwlerKeyPass.CreateLogin(Password.Password);
                Model.Open();
                Model.LoginView.ShowLogin = Visibility.Hidden;
                Password.Password = string.Empty;               
            }
            catch(Exception ex)
            {
                Model.LoginView.ErrorMessage = ex.Message;                
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Password.Focus();
            Keyboard.ClearFocus();
            Keyboard.Focus(Password);
        }

        private void Password_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
