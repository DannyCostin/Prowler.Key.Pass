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
    /// Interaction logic for KeyPassKeyOperation.xaml
    /// </summary>
    public partial class KeyPassKeyOperation : UserControl
    {
        public KeyPassFileModel Model
        {
            get { return (KeyPassFileModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(KeyPassFileModel), typeof(KeyPassKeyOperation));

        public KeyPassKeyOperation()
        {
            InitializeComponent();
        }

        private void SetUIPasswordBox(string password = "")
        {
            this.Password.Password = password;
        }

        private void btnOperation_Click(object sender, RoutedEventArgs e)
        {
            if (Model?.KeyOperationView == null)
            {
                return;
            }

            Model.KeyOperationView.Password = Password.Password;
            Password.Password = string.Empty;

            if (Model.KeyOperationView.IsNewEntry
                && Model.NewKey())
            {
                Model.KeyOperationView.ShowKeyOperationView = Visibility.Hidden;
                Model.ShowSaveControl = Visibility.Visible;
            }

            if (!Model.KeyOperationView.IsNewEntry
                && Model.EditKey())
            {
                Model.KeyOperationView.ShowKeyOperationView = Visibility.Hidden;
                Model.ShowSaveControl = Visibility.Visible;
            }
        }

        private void Password_KeyUp(object sender, KeyEventArgs e)
        {
            if (Model?.KeyOperationView == null)
            {
                return;
            }

            Model.KeyOperationView.HasPasswordEncryption = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (Model?.KeyOperationView == null)
            {
                return;
            }

            Model.KeyOperationView.HasPasswordEncryption = true;
            Model.KeyOperationView.ShowKeyOperationView = Visibility.Hidden;
            Model.CancelKeyOperation();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Model != null)
            {
                Model.SetUIPasswordBox = SetUIPasswordBox;
            }
        }

        private void btnGeneratePass_Click(object sender, RoutedEventArgs e)
        {
            this.Password.Password = Token(16);

            if (Model?.KeyOperationView == null)
            {
                return;
            }

            Model.KeyOperationView.HasPasswordEncryption = false;
        }

        private string Token(byte Length)
        {
            StringBuilder token = new StringBuilder();

            try
            {         
                char[] Chars = new char[] {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
                };

                Random Random = new Random();

                for (byte a = 0; a < Length; a++)
                {
                    token.Append(Chars[Random.Next(0, 61)]);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }

            return token.ToString();
        }
    }
}
