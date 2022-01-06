using Prowler.KeyPass.Presentation.Helper;
using Prowler.KeyPass.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for KeyPassAbout.xaml
    /// </summary>
    public partial class KeyPassAbout : UserControl
    {
        public KeyPassFileModel Model
        {
            get { return (KeyPassFileModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(KeyPassFileModel), typeof(KeyPassAbout));

        public KeyPassAbout()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (Model != null)
            {
                Model.ShowAboutView = Visibility.Hidden;
            }
        }

        private void PayPal_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
#pragma warning disable S1075 // URIs should not be hardcoded
                string url = @"https://www.paypal.com/donate/?hosted_button_id=79D39PJ2VKELW";
#pragma warning restore S1075 // URIs should not be hardcoded

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);                
            }
        }
    }
}
