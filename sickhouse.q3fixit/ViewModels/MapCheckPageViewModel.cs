using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using sickhouse.q3fixit.Utils;

namespace sickhouse.q3fixit.ViewModels
{
    public class MapCheckPageViewModel : BaseViewModel
    {
        public ICommand FixIt { get; private set; }

        private bool _folderOK;
        private string _q3Folder;
        public bool CopyMapsCheck { get; set; }

        public ObservableCollection<Info> Info
        {
            get { return _info; }
            set { _info = value; RaisePropertyChanged(() => Info); }
        }

        public string Q3Folder
        {
            get { return _q3Folder; }
            set
            {
                _q3Folder = value;
                FolderOK = Directory.Exists(_q3Folder) && File.Exists(_q3Folder + "//quake3.exe");
                RaisePropertyChanged(() => Q3Folder);
            }
        }

        public bool FolderOK
        {
            get { return _folderOK; }
            set { _folderOK = value; RaisePropertyChanged(() => FolderOK); }
        }

        private Action<string> _onRunAction;
        private ObservableCollection<Info> _info;

        public MapCheckPageViewModel(Action<string> onRunAction)
        {
            _onRunAction = onRunAction;
            FixIt = new RelayCommand(SaveExecute, CanExecuteSaveCommand);
            CopyMapsCheck = false;
        }

        private void SaveExecute(object obj)
        {
            // Read maps-file 
            var status = new List<Info>();
            var maplist = File.ReadAllLines("maps.txt").ToList();
            var okBrush = new SolidColorBrush(Colors.Green);
            var missingBrush = new SolidColorBrush(Colors.Red);
            foreach (var map in maplist)
            {
                var nameAndPath = map.Split(',');
                var folder = nameAndPath[0];
                var name = nameAndPath[1];
                var filename = _q3Folder + "//" + folder + "//" + name;
                var fileExists = File.Exists(filename);
                status.Add(new Info(){Description = map, Value = fileExists ? "Ok" : "Saknas", Color = fileExists ? okBrush : missingBrush});

                if (!fileExists)
                {
                    var copyFromFile = "maps/" + name;
                    File.Copy(copyFromFile, filename, true);
                }

            }

            Info = new ObservableCollection<Info>(status);

            // Read folder and enumerate all 

            // Copy file from folder in settings (relative program)

//            Directory dir = Directory.EnumerateFiles()

            _onRunAction.Invoke("");
        }

        private bool CanExecuteSaveCommand(object arg)
        {
            return FolderOK;
        }
    }

}
