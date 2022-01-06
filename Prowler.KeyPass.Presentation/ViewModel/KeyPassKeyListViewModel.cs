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
    public class KeyPassKeyListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void SendPropertyChangedNotifire(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set { _title = value; SendPropertyChangedNotifire(nameof(Title)); }
        }

        public string Password { get; set; } = string.Empty;

        private string _url = string.Empty;

        public string Url
        {
            get { return _url; }
            set { _url = value; SendPropertyChangedNotifire(nameof(Url)); }
        }

        private string _userName = string.Empty;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; SendPropertyChangedNotifire(nameof(UserName)); }
        }

        private string _note = string.Empty;

        public string Note
        {
            get { return _note; }
            set { _note = value; SendPropertyChangedNotifire(nameof(Note)); }
        }

        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; SendPropertyChangedNotifire(nameof(ErrorMessage)); }
        }

        private Visibility _showKeyOperationView = Visibility.Hidden;

        public Visibility ShowKeyOperationView
        {
            get { return _showKeyOperationView; }
            set
            {
                _showKeyOperationView = value;
                SendPropertyChangedNotifire(nameof(ShowKeyOperationView));

                if (value == Visibility.Visible)
                {
                    if (!IsNewEntry)
                    {
                        EditOperationBackup = this.MemberwiseClone() as KeyPassKeyListViewModel;
                    }
                    
                    Focus = true;
                }                               
            }
        }

        private string _buttonName = "Create";

        public string ButtonName
        {
            get { return _buttonName; }
            set { _buttonName = value; SendPropertyChangedNotifire(nameof(ButtonName)); }
        }

        private string _actionTitle = string.Empty;

        public string ActionTitle
        {
            get { return _actionTitle; }
            set { _actionTitle = value; SendPropertyChangedNotifire(nameof(ActionTitle)); }
        }


        private string _iconID = IconHelper.IconResource.First();

        public string IconID
        {
            get { return _iconID; }
            set { _iconID = value; SendPropertyChangedNotifire(nameof(IconID)); }
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
                    ActionTitle = "New Key";
                }
                else
                {
                    ButtonName = "Update";
                    ActionTitle = "Edit Key";
                }
            }
        }

        private string _groupName = string.Empty;

        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; SendPropertyChangedNotifire(nameof(GroupName)); }
        }

        private int _height = 50;

        public int Height
        {
            get { return _height; }
            set { _height = value; SendPropertyChangedNotifire(nameof(Height)); }
        }

        private bool _showDetails;

        public bool ShowDetails
        {
            get { return _showDetails; }
            set
            {
                _showDetails = value;
                SendPropertyChangedNotifire(nameof(ShowDetails));

                if (value)
                {
                    Height = 200;
                    DetailsControlText = " - ";
                }
                else
                {
                    Height = 50;
                    DetailsControlText = " + ";
                }
            }
        }

        private string _detailsControlText = "+";

        public string DetailsControlText
        {
            get { return _detailsControlText; }
            set { _detailsControlText = value; SendPropertyChangedNotifire(nameof(DetailsControlText)); }
        }

        private bool _focus;

        public bool Focus
        {
            get { return _focus; }
            set { _focus = value; SendPropertyChangedNotifire(nameof(Focus)); }
        }

        private Visibility _showGroupLabel = Visibility.Hidden;

        public Visibility ShowGroupLabel
        {
            get { return _showGroupLabel; }
            set { _showGroupLabel = value; SendPropertyChangedNotifire(nameof(Focus)); }
        }


        public bool HasPasswordEncryption { get; set; }

        public int ID { get; set; }
        
        public KeyPassKeyListViewModel? EditOperationBackup { get; set; }
    }
}
