using Prowler.KeyPass.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Prowler.KeyPass.Presentation.ViewModel
{
    public class KeyPassFileLoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void SendPropertyChangedNotifire(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public ILoginCipher? Login { get; set; }


        private Visibility _showLogin = Visibility.Hidden;

        public Visibility ShowLogin
        {
            get { return _showLogin; }
            set 
            { 
                _showLogin = value;

                if (value == Visibility.Visible)
                {
                    Focus = true;
                }

                SendPropertyChangedNotifire(nameof(ShowLogin));                 
            }
        }

        private bool _focus;

        public bool Focus
        {
            get { return _focus; }
            set { _focus = value; SendPropertyChangedNotifire(nameof(Focus)); }
        }


        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; SendPropertyChangedNotifire(nameof(ErrorMessage)); }
        }
    }
}
