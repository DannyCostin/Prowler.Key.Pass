using Prowler.KeyPass.Presentation.Helper;
using Prowler.KeyPass.Presentation.Model;
using Prowler.KeyPass.Presentation.ViewModel;
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
    /// Interaction logic for KeyPassKeyList.xaml
    /// </summary>
    public partial class KeyPassKeyList : UserControl
    {
        public KeyPassFileModel Model
        {
            get { return (KeyPassFileModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(KeyPassFileModel), typeof(KeyPassKeyList));

        public KeyPassKeyList()
        {
            InitializeComponent();
        }

        private void btnNewKey_Click(object sender, RoutedEventArgs e)
        {
            if(Model == null)
            {
                return;
            }

            Model.KeyOperationView = new KeyPassKeyListViewModel() { IsNewEntry = true};
            Model.KeyOperationView.ShowKeyOperationView = Visibility.Visible;
            Model.SetUIPasswordBox?.Invoke("");
        }

        private void btnGetUser_Click(object sender, RoutedEventArgs e)
        {
            if(Model != null)
            {
                Model.KeyListSelectedValue = (sender as Button)?.DataContext as KeyPassKeyListViewModel;
                Model.SetUserToClipboard();
            }          
        }

        private void btnGetPassword_Click(object sender, RoutedEventArgs e)
        {
            if(Model != null)
            {
                Model.KeyListSelectedValue = (sender as Button)?.DataContext as KeyPassKeyListViewModel;
                Model.SetPasswordToClipboard();
            }
        }

        private void btnEditEntry_Click(object sender, RoutedEventArgs e)
        {
            if(Model?.KeyOperationView == null)
            {
                return;
            }

            Model.KeyListSelectedValue = (sender as Button)?.DataContext as KeyPassKeyListViewModel;
            Model.KeyOperationView = Model.KeyListSelectedValue;                        

            if (Model.KeyOperationView != null)
            {
                Model.SetUIPasswordBox?.Invoke(Model.KeyOperationView.Password);
                Model.KeyOperationView.IsNewEntry = false;
                Model.KeyOperationView.ShowKeyOperationView = Visibility.Visible;
            }            
        }

        private void btnDeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            if (Model?.KeyOperationView == null)
            {
                return;
            }

            Model.KeyListSelectedValue = (sender as Button)?.DataContext as KeyPassKeyListViewModel;
            Model.DeleteKey();
        }

        private void btnShowDetails_Click(object sender, RoutedEventArgs e)
        {
            if(Model != null)
            {
               var entity = (sender as Button)?.DataContext as KeyPassKeyListViewModel;

               Model.KeyListSelectedValue = entity;

               if (entity != null)
               {
                    if (entity.ShowDetails)
                    {
                        entity.ShowDetails = false;

                        return;
                    }

                    if (!entity.ShowDetails)
                    {
                        entity.ShowDetails = true;
                    }
               }
            }
        }  

        private void Link_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var model = (sender as TextBlock)?.DataContext as KeyPassKeyListViewModel;                

                if(model == null || string.IsNullOrEmpty(model.Url))
                {
                    return;
                }

                var url = model.Url;

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
            catch
            {
                Console.WriteLine("Invalid url");
            }
        }

        private void Link_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var model = (sender as TextBlock)?.DataContext as KeyPassKeyListViewModel;

                if (model == null || string.IsNullOrEmpty(model.Url))
                {
                    return;
                }

                Clipboard.SetText(model.Url);
            }
            catch
            {
                Console.WriteLine("Invalid url");
            }
        }

        private void KeyList_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.A && Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    Model.SetUserToClipboard();
                }

                if (e.Key == Key.S && Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    Model.SetPasswordToClipboard();
                }

                if (e.Key == Key.Delete)
                {
                    Model.DeleteKey();
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());                
            }
        }

        private void EditMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Model?.KeyOperationView == null)
            {
                return;
            }

            Model.KeyListSelectedValue = this.KeyList.SelectedItem as KeyPassKeyListViewModel;
            Model.KeyOperationView = Model.KeyListSelectedValue;

            if (Model.KeyOperationView != null)
            {
                Model.SetUIPasswordBox?.Invoke(Model.KeyOperationView.Password);
                Model.KeyOperationView.IsNewEntry = false;
                Model.KeyOperationView.ShowKeyOperationView = Visibility.Visible;
            }
        }

        private void CopyUserMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Model != null)
            {
                Model.KeyListSelectedValue = this.KeyList.SelectedItem as KeyPassKeyListViewModel;
                Model.SetUserToClipboard();
            }
        }

        private void CopyPasswordMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Model != null)
            {
                Model.KeyListSelectedValue = this.KeyList.SelectedItem as KeyPassKeyListViewModel;
                Model.SetPasswordToClipboard();
            }
        }

        private void DeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Model?.KeyOperationView == null)
            {
                return;
            }

            Model.KeyListSelectedValue = this.KeyList.SelectedItem as KeyPassKeyListViewModel;
            Model.DeleteKey();
        }
    }
}
