using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Prowler.KeyPass.Core;
using Prowler.KeyPass.Presentation.ViewModel;

namespace Prowler.KeyPass.Presentation.Model
{
    public class KeyPassFileModel : INotifyPropertyChanged
    {
        public const string DialogFilter = "Prowler KeyPass Files | *.pkp";

        private readonly IKeyPass _keyPassHandler;
        private IKeyPassFile? _keyPassFile;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void SendPropertyChangedNotifire(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public string? File { get; set; }

        private Visibility _showKeyList = Visibility.Hidden;

        public Visibility ShowKeyList
        {
            get { return _showKeyList; }
            set { _showKeyList = value; SendPropertyChangedNotifire(nameof(ShowKeyList)); }
        }


        private Visibility _showSaveControl = Visibility.Hidden;

        public Visibility ShowSaveControl
        {
            get { return _showSaveControl; }
            set { _showSaveControl = value; SendPropertyChangedNotifire(nameof(ShowSaveControl)); }
        }

        private KeyPassFileLoginViewModel _loginView = new KeyPassFileLoginViewModel();

        public KeyPassFileLoginViewModel LoginView
        {
            get { return _loginView; }
            set { _loginView = value; SendPropertyChangedNotifire(nameof(LoginView)); }
        }

        private KeyPassNewFileViewModel _newFileView = new KeyPassNewFileViewModel();

        public KeyPassNewFileViewModel NewFileView
        {
            get { return _newFileView; }
            set { _newFileView = value; SendPropertyChangedNotifire(nameof(NewFileView)); }
        }

        private KeyPassKeyListViewModel? _keyOperationView = new KeyPassKeyListViewModel();

        public KeyPassKeyListViewModel? KeyOperationView
        {
            get { return _keyOperationView; }
            set { _keyOperationView = value; SendPropertyChangedNotifire(nameof(KeyOperationView)); }
        }


        private KeyPassGroupListViewModel? _groupOperationView = new KeyPassGroupListViewModel();

        public KeyPassGroupListViewModel? GroupOperationView
        {
            get { return _groupOperationView; }
            set { _groupOperationView = value; SendPropertyChangedNotifire(nameof(GroupOperationView)); }
        }

        private KeyPassGroupListViewModel? _groupListSelectedValue;

        public KeyPassGroupListViewModel? GroupListSelectedValue
        {
            get { return _groupListSelectedValue; }
            set
            {
                _groupListSelectedValue = value;
                SetKeysByGroup(value);
                SendPropertyChangedNotifire(nameof(GroupListSelectedValue));
                ShowKeyList = Visibility.Visible;
            }
        }

        private KeyPassKeyListViewModel? _keyListSelectedValue;

        public KeyPassKeyListViewModel? KeyListSelectedValue
        {
            get { return _keyListSelectedValue; }
            set { _keyListSelectedValue = value; SendPropertyChangedNotifire(nameof(KeyListSelectedValue)); }
        }


        private ObservableCollection<KeyPassGroupListViewModel>? _groupList;

        public ObservableCollection<KeyPassGroupListViewModel>? GroupList
        {
            get { return _groupList; }
            set { _groupList = value; SendPropertyChangedNotifire(nameof(GroupList)); }
        }

        private ObservableCollection<KeyPassKeyListViewModel>? _keyList;

        public ObservableCollection<KeyPassKeyListViewModel>? KeyList
        {
            get { return _keyList; }
            set { _keyList = value; SendPropertyChangedNotifire(nameof(KeyList)); }
        }

        private ObservableCollection<KeyPassRecentFileViewModel> _recentList = new ObservableCollection<KeyPassRecentFileViewModel>();

        public ObservableCollection<KeyPassRecentFileViewModel> RecentList
        {
            get { return _recentList; }
            set { _recentList = value; SendPropertyChangedNotifire(nameof(RecentList)); }
        }

        private KeyPassRecentFileViewModel? _selectedRecentFile;

        public KeyPassRecentFileViewModel? SelectedRecentFile
        {
            get { return _selectedRecentFile; }
            set { _selectedRecentFile = value; SendPropertyChangedNotifire(nameof(SelectedRecentFile)); }
        }

        private Visibility _showAboutView = Visibility.Hidden;

        public Visibility ShowAboutView
        {
            get { return _showAboutView; }
            set { _showAboutView = value; SendPropertyChangedNotifire(nameof(ShowAboutView)); }
        }

        private string? _openFileName;

        public string? OpenFileName
        {
            get { return _openFileName; }
            set { _openFileName = value; SendPropertyChangedNotifire(nameof(OpenFileName)); }
        }


        public Action<string>? SetUIPasswordBox { get; set; }

        public KeyPassFileModel()
        {
            _keyPassHandler = ProwlerKeyPass.ProwlerKeyPassFile();
        }        

        public void Open()
        {
            if (LoginView?.Login == null || File == null)
            {
                return;
            }

            _keyPassFile = _keyPassHandler.Open(File, LoginView.Login).Handle();
            GroupList = GetGroups();
            KeyList?.Clear();
            ShowKeyList = Visibility.Hidden;
            ShowSaveControl = Visibility.Hidden;
            AddFileToRecentList(File);
            SaveRecentList();

            if(GroupList != null && GroupList.Any())
            {
                GroupListSelectedValue = GroupList.First();                
            }

            OpenFileName = $" - {Path.GetFileNameWithoutExtension(File)}";
        }

        public void AddFileToRecentList(string file)
        {
            if (System.IO.File.Exists(file)
                && !RecentList.Any(i => i.FilePath == file))
            {
                RecentList.Insert(0, new KeyPassRecentFileViewModel
                {
                    FileName = System.IO.Path.GetFileNameWithoutExtension(file),
                    FilePath = file,
                    Directory = Path.GetFileName(Path.GetDirectoryName(file))
                });
            }
        }
        
        public void RemoveFileFromRecentList()
        {
            if (SelectedRecentFile != null)
            {
                RecentList.Remove(SelectedRecentFile);
                SaveRecentList();
            }
        }

        public void SaveRecentList()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Prowler Key Pass");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string jsonString = JsonSerializer.Serialize(RecentList.ToList());

            System.IO.File.WriteAllText(Path.Combine(folder, "prowlerRecent.pst"), jsonString);
        }

        public void LoadRecentList()
        {
            string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Prowler Key Pass", "prowlerRecent.pst");

            if (System.IO.File.Exists(file))
            {
                var jsonString = System.IO.File.ReadAllText(file);
                var recentList = JsonSerializer.Deserialize(jsonString, typeof(List<KeyPassRecentFileViewModel>)) as List<KeyPassRecentFileViewModel>;

                if (recentList != null)
                {
                    recentList.ForEach(i => RecentList.Add(i));
                }
            }
        }

        public void NewFile()
        {
            if (LoginView?.Login == null || File == null)
            {
                return;
            }

            _keyPassFile = _keyPassHandler.New(LoginView.Login).Handle();

            if (_keyPassFile != null)
            {
                _keyPassHandler.Save(File, LoginView.Login, _keyPassFile).Handle();
                GroupList = GetGroups();
                ShowKeyList = Visibility.Hidden;
                ShowSaveControl = Visibility.Hidden;
                AddFileToRecentList(File);
                SaveRecentList();
            }
        }

        public void Save()
        {
            if (LoginView?.Login == null || File == null)
            {
                return;
            }

            if (_keyPassFile != null)
            {
                _keyPassHandler.Save(File, LoginView.Login, _keyPassFile).Handle();
            }
        }

        public void SetUserToClipboard()
        {
            if (KeyListSelectedValue != null)
            {
                Clipboard.SetDataObject(KeyListSelectedValue.UserName);
            }
        }

        public void SetPasswordToClipboard()
        {
            if (KeyListSelectedValue != null && _keyPassFile != null && LoginView?.Login != null)
            {
                var key = _keyPassFile.DataSource.First(i => i.ID == KeyListSelectedValue.ID);
                Clipboard.SetDataObject(key.GetPassword(LoginView.Login));
            }
        }

        public void CancelGroupOperation()
        {
            if (GroupListSelectedValue?.EditOperationBackup != null)
            {
                GroupListSelectedValue.Name = GroupListSelectedValue.EditOperationBackup.Name;
                GroupListSelectedValue.IconID = GroupListSelectedValue.EditOperationBackup.IconID;
                GroupListSelectedValue.ErrorMessage = GroupListSelectedValue.EditOperationBackup.ErrorMessage;
                GroupListSelectedValue.EditOperationBackup = null;
            }
        }

        public void CancelKeyOperation()
        {
            if (KeyListSelectedValue?.EditOperationBackup != null)
            {
                KeyListSelectedValue.Title = KeyListSelectedValue.EditOperationBackup.Title;
                KeyListSelectedValue.ErrorMessage = KeyListSelectedValue.EditOperationBackup.ErrorMessage;
                KeyListSelectedValue.Note = KeyListSelectedValue.EditOperationBackup.Note;
                KeyListSelectedValue.UserName = KeyListSelectedValue.EditOperationBackup.UserName;
                KeyListSelectedValue.Password = KeyListSelectedValue.EditOperationBackup.Password;
                KeyListSelectedValue.Url = KeyListSelectedValue.EditOperationBackup.Url;
                KeyListSelectedValue.IconID = KeyListSelectedValue.EditOperationBackup.IconID;
                KeyListSelectedValue.EditOperationBackup = null;
            }
        }

        private bool IsGroupOperationValid()
        {
            if (GroupOperationView == null || GroupList == null)
            {
                return false;
            }

            GroupOperationView.ErrorMessage = string.Empty;

            if (String.IsNullOrEmpty(GroupOperationView.Name))
            {
                GroupOperationView.ErrorMessage = "Invalid group name, group name cannot be empty.";

                return false;
            }

            if (GroupOperationView.IsNewEntry && GroupList.Any(i => i.Name == GroupOperationView.Name))
            {
                GroupOperationView.ErrorMessage = "Invalid group name, group name already exists.";

                return false;
            }

            if (!GroupOperationView.IsNewEntry && GroupList.Count(i => i.Name == GroupOperationView.Name) > 1)
            {
                GroupOperationView.ErrorMessage = "Invalid group name, group name already exists.";

                return false;
            }

            return true;
        }

        private bool IsKeyOperationValid()
        {
            if (KeyOperationView == null || KeyList == null || GroupListSelectedValue == null)
            {
                return false;
            }

            KeyOperationView.ErrorMessage = string.Empty;

            if (String.IsNullOrEmpty(KeyOperationView.Title))
            {
                KeyOperationView.ErrorMessage = "Invalid title name, title name cannot be empty.";

                return false;
            }

            if (KeyOperationView.IsNewEntry && KeyList.Any(i => i.Title == KeyOperationView.Title && i.GroupName == GroupListSelectedValue.Name))
            {
                KeyOperationView.ErrorMessage = "Invalid title name, title name already exists";

                return false;
            }

            if (!KeyOperationView.IsNewEntry && KeyList.Count(i => i.Title == KeyOperationView.Title && i.GroupName == KeyOperationView.GroupName) > 1)
            {
                KeyOperationView.ErrorMessage = "Invalid title name, title name already exists";

                return false;
            }

            return true;
        }

        public bool NewKey()
        {
            if (KeyOperationView == null
                || KeyList == null
                || GroupListSelectedValue == null
                || LoginView?.Login == null
                || _keyPassFile == null)
            {
                return false;
            }

            if (!IsKeyOperationValid())
            {
                return false;
            }

            KeyOperationView.GroupName = GroupListSelectedValue.Name;

            KeyList.Add(KeyOperationView);
            var entry = _keyPassFile.Add();
            entry.Group = GroupListSelectedValue.Name;
            entry.UserName = KeyOperationView.UserName;
            entry.GroupIconID = GroupListSelectedValue.IconID;
            entry.KeyIconID = KeyOperationView.IconID;
            entry.Notes = KeyOperationView.Note;
            entry.Title = KeyOperationView.Title;
            entry.Url = KeyOperationView.Url;
            entry.UserName = KeyOperationView.UserName;
            entry.SetPassword(LoginView.Login, KeyOperationView.Password);

            KeyOperationView.ID = entry.ID;
            KeyOperationView.Password = entry.Password;
            KeyOperationView.IsNewEntry = false;
            KeyOperationView.HasPasswordEncryption = true;

            if (KeyListSelectedValue != null)
            {
                KeyListSelectedValue.EditOperationBackup = null;
            }

            return true;
        }

        public bool EditKey()
        {
            if (KeyOperationView == null
                || KeyList == null
                || GroupListSelectedValue == null
                || _keyPassFile == null
                || LoginView?.Login == null)
            {
                return false;
            }

            if (!IsKeyOperationValid())
            {
                return false;
            }

            KeyOperationView.GroupName = GroupListSelectedValue.Name;
            var entry = _keyPassFile.DataSource.First(i => i.ID == KeyOperationView.ID);

            entry.UserName = KeyOperationView.UserName;
            entry.GroupIconID = GroupListSelectedValue.IconID;
            entry.KeyIconID = KeyOperationView.IconID;
            entry.Notes = KeyOperationView.Note;
            entry.Title = KeyOperationView.Title;
            entry.Url = KeyOperationView.Url;
            entry.UserName = KeyOperationView.UserName;

            if (!KeyOperationView.HasPasswordEncryption)
            {
                entry.SetPassword(LoginView.Login, KeyOperationView.Password);
                KeyOperationView.Password = entry.Password;
                KeyOperationView.HasPasswordEncryption = true;
            }

            if (KeyListSelectedValue?.EditOperationBackup != null)
            {
                KeyListSelectedValue.EditOperationBackup = null;
            }

            return true;
        }

        public bool NewGroup()
        {
            if (GroupOperationView == null || GroupList == null)
            {
                return false;
            }

            if (!IsGroupOperationValid())
            {
                return false;
            }

            GroupList.Add(GroupOperationView);

            return true;
        }

        public bool EditGroup()
        {
            if (GroupOperationView == null || GroupList == null)
            {
                return false;
            }

            if (!IsGroupOperationValid())
            {
                return false;
            }

            if (KeyList != null)
            {
                var keyList = KeyList.Where(i => i.GroupName == GroupOperationView.EditOperationBackup?.Name);

                foreach (var key in keyList)
                {
                    key.GroupName = GroupOperationView.Name;
                }
            }

            if (_keyPassFile != null)
            {
                var keyListPass = _keyPassFile.DataSource.Where(i => i.Group == GroupOperationView.EditOperationBackup?.Name);

                foreach (var key in keyListPass)
                {
                    key.Group = GroupOperationView.Name;
                    key.GroupIconID = GroupOperationView.IconID;
                }
            }

            if (GroupListSelectedValue?.EditOperationBackup != null)
            {
                GroupListSelectedValue.EditOperationBackup = null;
            }

            return true;
        }

        public void DeleteGroup()
        {
            if (GroupListSelectedValue == null || GroupList == null)
            {
                return;
            }

            if (KeyList != null)
            {
                var keyList = KeyList.Where(i => i.GroupName == GroupListSelectedValue.Name).ToList();

                foreach (var key in keyList)
                {
                    KeyList.Remove(key);
                }
            }

            if (_keyPassFile != null)
            {
                var keyList = _keyPassFile.DataSource.Where(i => i.Group == GroupListSelectedValue.Name).ToList();

                foreach (var key in keyList)
                {
                    _keyPassFile.Delete(key);
                }
            }

            GroupList.Remove(GroupListSelectedValue);

            ShowSaveControl = Visibility.Visible;
            ShowKeyList = Visibility.Hidden;
        }

        public void DeleteKey()
        {
            if (KeyListSelectedValue == null)
            {
                return;
            }

            if (_keyPassFile != null)
            {
                var key = _keyPassFile.DataSource.First(i => i.ID == KeyListSelectedValue.ID);
                _keyPassFile.Delete(key);
                ShowSaveControl = Visibility.Visible;
            }

            if (KeyList != null)
            {
                KeyList.Remove(KeyListSelectedValue);
            }
        }

        public ObservableCollection<KeyPassGroupListViewModel> GetGroups()
        {
            if (_keyPassFile == null)
            {
                return new ObservableCollection<KeyPassGroupListViewModel>();
            }

            return new ObservableCollection<KeyPassGroupListViewModel>(_keyPassFile.DataSource.Select(i => new { i.Group, i.GroupIconID }).Distinct()
                                                                                              .Select(s => new KeyPassGroupListViewModel { Name = s.Group, IconID = s.GroupIconID })
                                                                                              .ToList());
        }

        public void SetKeysByGroup(KeyPassGroupListViewModel? group)
        {
            if (_keyPassFile == null || group == null)
            {
                return;
            }

            KeyList = new ObservableCollection<KeyPassKeyListViewModel>(_keyPassFile.DataSource.Where(i => i.Group == group.Name)
                                          .Select(i => new KeyPassKeyListViewModel
                                          {
                                              Title = i.Title,
                                              Password = i.Password,
                                              Url = i.Url,
                                              Note = i.Notes,
                                              UserName = i.UserName,
                                              GroupName = i.Group,
                                              HasPasswordEncryption = true,
                                              ID = i.ID,
                                              IconID = i.KeyIconID
                                          })
                                          .ToList());
        }

        public void SearchKeys(string value)
        {
            if (_keyPassFile == null)
            {
                return;
            }

            if (String.IsNullOrEmpty(value))
            {
                SetKeysByGroup(GroupListSelectedValue);

                return;
            }

            KeyList = new ObservableCollection<KeyPassKeyListViewModel>(_keyPassFile.DataSource.Where(i => i.Title.ToLower().Contains(value.ToLower()))
                                          .Select(i => new KeyPassKeyListViewModel
                                          {
                                              Title = i.Title,
                                              Password = i.Password,
                                              Url = i.Url,
                                              Note = i.Notes,
                                              UserName = i.UserName,
                                              GroupName = i.Group,
                                              HasPasswordEncryption = true,
                                              ID = i.ID,
                                              IconID = i.KeyIconID,
                                              ShowGroupLabel = Visibility.Visible
                                          })
                                          .ToList());
        }
    }
}
