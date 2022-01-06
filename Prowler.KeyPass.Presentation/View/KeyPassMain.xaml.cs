using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Prowler.KeyPass.Presentation.Helper;
using Prowler.KeyPass.Presentation.Model;
using Prowler.KeyPass.Presentation.ViewModel;

namespace Prowler.KeyPass.Presentation.View
{
    /// <summary>
    /// Interaction logic for KeyPassMain.xaml
    /// </summary>
    public partial class KeyPassMain : UserControl
    {

        KeyPassFileModel _keyPassModel;

        public bool HasUnSavedChanges
        {
            get
            {
                return _keyPassModel?.ShowSaveControl == Visibility.Visible;
            }
        }

        public KeyPassMain()
        {
            InitializeComponent();

            _keyPassModel = new KeyPassFileModel();
            this.DataContext = _keyPassModel;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_keyPassModel.ShowSaveControl == Visibility.Visible
                   && MessageBox.Show("Do you want to save the changes?", "Prowler Key Pass", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _keyPassModel.Save();
                }

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = KeyPassFileModel.DialogFilter;

                if (openFileDialog.ShowDialog() == true)
                {
                    this.Container.Children.Clear();
                    var editor = new KeyPassFileEditor();
                    editor.Model = _keyPassModel;
                    this.Container.Children.Add(editor);
                    editor.OpenFile(openFileDialog.FileName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {           
            this.Container.Children.Clear();
            var editor = new KeyPassFileEditor();
            editor.Model = _keyPassModel;
            this.Container.Children.Add(editor);

            var newFile = new KeyPassNewFile();
            newFile.Model = _keyPassModel;
            this.Container.Children.Add(newFile);            
            _keyPassModel.LoginView.ShowLogin = Visibility.Hidden;
            _keyPassModel.NewFileView.ShowNewFile = Visibility.Visible;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _keyPassModel.Save();
                _keyPassModel.ShowSaveControl = Visibility.Hidden;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveChanges()
        {
            _keyPassModel.Save();
            _keyPassModel.ShowSaveControl = Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BackgroundImage.Source = TryFindResource(IconHelper.GetBackgroundImage()) as ImageSource;
                _keyPassModel.LoadRecentList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _keyPassModel.SelectedRecentFile = (sender as Button)?.DataContext as KeyPassRecentFileViewModel;
                _keyPassModel.RemoveFileFromRecentList();
                e.Handled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RecentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {                    
                    _keyPassModel.SelectedRecentFile = RecentList.SelectedValue as KeyPassRecentFileViewModel;

                    if(_keyPassModel.SelectedRecentFile == null)
                    {
                        return;
                    }

                    if (System.IO.File.Exists(_keyPassModel.SelectedRecentFile?.FilePath))
                    {
                        this.Container.Children.Clear();
                        var editor = new KeyPassFileEditor();
                        editor.Model = _keyPassModel;
                        this.Container.Children.Add(editor);
                        editor.OpenFile(_keyPassModel.SelectedRecentFile.FilePath);                       
                    }
                    else
                    {
                        MessageBox.Show($"Cannot find file {_keyPassModel.SelectedRecentFile?.FilePath}", "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            _keyPassModel.ShowAboutView = Visibility.Visible;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var process = new Process();
                process.StartInfo = new ProcessStartInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Manual\prowler_key_pass_help.pdf"))
                {
                    UseShellExecute = true
                };

                process.Start();                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
