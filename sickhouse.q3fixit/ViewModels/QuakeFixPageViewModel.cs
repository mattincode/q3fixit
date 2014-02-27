using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using sickhouse.q3fixit.Utils;

namespace sickhouse.q3fixit.ViewModels
{
    public class QuakeFixPageViewModel : BaseViewModel
    {
        public ICommand FixIt { get; private set; }

        private bool _folderOK;
        private string _q3Folder;
        public Model PlayerModel { get; set; }
        public ObservableCollection<Model> PlayerList { get; set; }
        public bool ResolutionCheck { get; set; }
        public bool BrightnessCheck { get; set; }
        public bool AdapterCheck { get; set; }
        public bool LanCheck { get; set; }
        public bool MemoryCheck { get; set; }
        public bool ModelCheck { get; set; }
        public bool FpsCheck { get; set; }

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

        private Action<object> _onRunAction; 
        public QuakeFixPageViewModel(Action<object> onRunAction)
        {
            _onRunAction = onRunAction;
            var players = new List<Model>() { new Model() { Modelname = "titte", Name = "Titte" }, new Model() { Modelname = "agent", Name = "Agent" }, 
                          new Model() { Modelname = "cleansweep", Name = "Cleansweep" }, new Model() { Modelname = "donhakon", Name = "Don Hakon" },
                          new Model() { Modelname = "atmoic", Name = "Atomic" }, new Model() { Modelname = "butcher", Name = "Butcher" }, 
                          new Model() { Modelname = "finalinsult", Name = "Final Insult" }, new Model() { Modelname = "hanseman", Name = "Hanseman" }, 
                          new Model() { Modelname = "herton", Name = "Herton" }, new Model() { Modelname = "joensson", Name = "Joensson" }, 
                          new Model() { Modelname = "mega", Name = "Mega" }, new Model() { Modelname = "mrego", Name = "Mr Ego" },
                          new Model() { Modelname = "netman", Name = "Netman"}, new Model(){Modelname = "titte", Name = "Titte"}};
            //"Titte", "Mr Ego", "Cleansweep", "Final Insult", "Joensson", "Hanseman", "Mega", "Atomic", "Butcher", "Netman", "Herton", "RunningRiot", "Agent" 
            PlayerList = new ObservableCollection<Model>(players);
            FixIt = new RelayCommand(SaveExecute, CanExecuteSaveCommand);
            ResolutionCheck = true;
            BrightnessCheck = true;
            AdapterCheck = false;
            LanCheck = true;
            MemoryCheck = true;
            ModelCheck = true;
            FpsCheck = true;
        }

        private void SaveExecute(object obj)
        {
            var cfg = new Q3ConfigHandler(Q3Folder);

            if (ResolutionCheck)
                cfg.SetCustomResolution(GraphicsUtil.GetPrimaryScreenResolutionObj());
            if (BrightnessCheck)
                cfg.SetBrightness();
            if (AdapterCheck)
                cfg.SetGraphicsCard(GraphicsUtil.GetAdapterName());
            if (LanCheck)
                cfg.SetNetworkToLan();
            if (MemoryCheck)
                cfg.SetMemoryAlloc();
            if (ModelCheck)
                cfg.SetPlayerModel(PlayerModel.Name, PlayerModel.Modelname);
            if (FpsCheck)
                cfg.SetMaxFps(125);

            _onRunAction.Invoke(obj);
        }

        private bool CanExecuteSaveCommand(object arg)
        {
            return FolderOK && (!ModelCheck || PlayerModel != null);
        }
    }

    public class Model
    {
        public string Name { get; set; }
        public string Modelname  { get; set; }

    }
}
