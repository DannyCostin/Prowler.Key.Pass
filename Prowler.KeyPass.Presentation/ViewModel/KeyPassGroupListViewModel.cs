using Prowler.KeyPass.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Prowler.KeyPass.Presentation.ViewModel
{
    public class KeyPassGroupListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void SendPropertyChangedNotifire(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private Visibility _showGroupOperationView = Visibility.Hidden;

        public Visibility ShowGroupOperationView
        {
            get { return _showGroupOperationView; }
            set 
            { 
                _showGroupOperationView = value;
                SendPropertyChangedNotifire(nameof(ShowGroupOperationView));

                if (value == Visibility.Visible)
                {
                    if (!IsNewEntry)
                    {
                        EditOperationBackup = this.MemberwiseClone() as KeyPassGroupListViewModel;
                    }
                    
                    Focus = true;
                }                
            }
        }

        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; SendPropertyChangedNotifire(nameof(ErrorMessage)); }
        }

        private string _name = string.Empty;

        public string Name
        {
            get { return _name; }
            set { _name = value; SendPropertyChangedNotifire(nameof(Name)); }
        }

        private string _iconID = IconHelper.IconResource.First();

        public string IconID
        {
            get { return _iconID; }
            set { _iconID = value; SendPropertyChangedNotifire(nameof(IconID)); }
        }

        private string _buttonName = "Create";

        public string ButtonName
        {
            get { return _buttonName; }
            set { _buttonName = value; SendPropertyChangedNotifire(nameof(ButtonName)); }
        }

        private bool _isNewEntry;

        public bool IsNewEntry
        {
            get { return _isNewEntry; }
            set
            {
                _isNewEntry = value;

                if (value)
                {
                    ButtonName = "Create";
                }
                else
                {
                    ButtonName = "Update";
                }
            }
        }

        private bool _focus;

        public bool Focus
        {
            get { return _focus; }
            set { _focus = value; SendPropertyChangedNotifire(nameof(Focus)); }
        }

        public KeyPassGroupListViewModel? EditOperationBackup { get; set; }
    }
}
