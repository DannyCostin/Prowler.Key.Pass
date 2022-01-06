using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prowler.KeyPass.Presentation.ViewModel
{
    public class KeyPassRecentFileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void SendPropertyChangedNotifire(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string _fileName = string.Empty;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; SendPropertyChangedNotifire(nameof(FileName)); }
        }

        public string FilePath { get; set; } = string.Empty;

        private string? _directory;

        public string? Directory
        {
            get { return _directory; }
            set { _directory = value; SendPropertyChangedNotifire(nameof(Directory)); }
        }       
    }
}
