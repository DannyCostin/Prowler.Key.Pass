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
using Microsoft.Win32;

namespace Prowler.KeyPass.Presentation.View
{
    /// <summary>
    /// Interaction logic for KeyPassNewFile.xaml
    /// </summary>
    public partial class KeyPassNewFile : UserControl
    {
        public KeyPassFileModel Model
        {
            get { return (KeyPassFileModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(KeyPassFileModel), typeof(KeyPassNewFile));

        public KeyPassNewFile()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Model == null)
                {
                    return;
                }

                Model.NewFileView.ErrorMessage = string.Empty;

                if (this.Password.Password != this.PasswordConfirm.Password)
                {
                    Model.NewFileView.ErrorMessage = "The password does not match!";

                    return;
                }

                if (String.IsNullOrEmpty(this.Password.Password))
                {
                    Model.NewFileView.ErrorMessage = "The password cannot be empty!";

                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = KeyPassFileModel.DialogFilter;

                if (saveFileDialog.ShowDialog() == true)
                {
                    Model.File = saveFileDialog.FileName;
                    Model.NewFileView.ShowNewFile = Visibility.Hidden;
                    Model.LoginView.Login = Prowler.KeyPass.Core.ProwlerKeyPass.CreateLogin(Password.Password);
                    Model.NewFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Password.Focus();
            Keyboard.ClearFocus();
            Keyboard.Focus(Password);
        }

        private void PasswordConfirm_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
                Keyboard.Focus(Password);
                btnCreate_Click(sender, e);                
            }
           
            e.Handled = true;
        }
    }
}
