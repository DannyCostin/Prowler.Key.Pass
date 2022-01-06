using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Prowler.KeyPass.Presentation.ViewModel
{
    public class KeyPassNewFileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void SendPropertyChangedNotifire(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; SendPropertyChangedNotifire(nameof(ErrorMessage)); }
        }

        private Visibility _showNewFile = Visibility.Hidden;

        public Visibility ShowNewFile
        {
            get { return _showNewFile; }
            set 
            { 
                _showNewFile = value; 
                SendPropertyChangedNotifire(nameof(ShowNewFile));
                
                if(value == Visibility.Visible)
                {
                    Focus = true;
                }
            }
        }

        private bool _focus;

        public bool Focus
        {
            get { return _focus; }
            set { _focus = value; SendPropertyChangedNotifire(nameof(Focus)); }
        }

    }
}
