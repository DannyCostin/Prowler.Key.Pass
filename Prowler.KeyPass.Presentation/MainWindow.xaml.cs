using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using Prowler.KeyPass.Core;
using Prowler.KeyPass.Presentation.Entity;
using Prowler.KeyPass.Presentation.Model;

namespace Prowler.KeyPass.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        ConfigEntity? config = new ConfigEntity();
        
        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void WindowMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void WindowState_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;

                return;
            }

            this.WindowState = WindowState.Maximized;
        }

        private void WindowClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (KeyPass.HasUnSavedChanges
                    && MessageBox.Show("Do you want to save the changes?", "Prowler Key Pass", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    KeyPass.SaveChanges();
                }

                Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save()
        {
            try
            {
                if (config == null)
                {
                    config = new ConfigEntity();
                    config.Save(this);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                config = ConfigEntity.Load(this);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Prowler Key Pass", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
