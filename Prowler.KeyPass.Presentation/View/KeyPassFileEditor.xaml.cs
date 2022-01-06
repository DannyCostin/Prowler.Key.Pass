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
    /// Interaction logic for KeyPassFileEditor.xaml
    /// </summary>
    public partial class KeyPassFileEditor : UserControl
    {
        public KeyPassFileModel Model
        {
            get { return (KeyPassFileModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(KeyPassFileModel), typeof(KeyPassFileEditor));

        public KeyPassFileEditor()
        {
            InitializeComponent();                     
        }

        public void OpenFile(string file)
        {
            Model.File = file;
            Model.LoginView.ShowLogin = Visibility.Visible;
            this.ContainerMiddle.Children.Clear();
            var loginControl = new KeyPassFileLogin();
            loginControl.Model = Model;
            this.ContainerMiddle.Children.Add(loginControl);            
        }

        string? lastProcessed;
        private async void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Model?.KeyList == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(TextSearch.Text))
                {
                    lastProcessed = null;
                }

                async Task<bool> UserKeepsTyping()
                {
                    string txt = TextSearch.Text;   // remember text
                    await Task.Delay(500);        // wait some
                    return txt != TextSearch.Text;  // return that text chaged or not
                }

                if (await UserKeepsTyping() || TextSearch.Text == lastProcessed)
                {
                    return;
                }

                lastProcessed = TextSearch.Text;
                Model.SearchKeys(TextSearch.Text);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);                
            }
        }       
    }
}
