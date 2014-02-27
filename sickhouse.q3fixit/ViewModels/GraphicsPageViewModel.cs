using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using sickhouse.q3fixit.Utils;

namespace sickhouse.q3fixit.ViewModels
{
    public class GraphicsPageViewModel : BaseViewModel
    {
        private Resolution _primaryScreenResolution;
        private ObservableCollection<Info> _graphicsInfo;

        public ObservableCollection<Info> GraphicsInfo
        {
            get { return _graphicsInfo; }
            set { _graphicsInfo = value; RaisePropertyChanged(() => GraphicsInfo); }
        }


        public Resolution PrimaryScreenResolution
        {
            get { return _primaryScreenResolution; }
            set { _primaryScreenResolution = value; RaisePropertyChanged(() => PrimaryScreenResolution); }
        }

        public GraphicsPageViewModel(Action<object> onLoadAction)
        {
            LoadData(onLoadAction);
        }

        private void LoadData(Action<object> onLoadAction)
        {            
            var info = new List<Info>();
            Task.Factory.StartNew(() => {
                PrimaryScreenResolution = GraphicsUtil.GetPrimaryScreenResolutionObj();
                info.Add(new Info() {Description= "Primär skärmupplösning", Value= GraphicsUtil.GetPrimaryScreenResolution()});
                info.Add(new Info() { Description = "Primär GPU", Value = GraphicsUtil.GetAdapterName() });
                info.Add(new Info() { Description = "Drivrutin", Value = GraphicsUtil.GetPrimaryAdapterDriver() });
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).ContinueWith((task =>
            {
                GraphicsInfo = new ObservableCollection<Info>(info);
                onLoadAction.Invoke(null);

            }), TaskScheduler.FromCurrentSynchronizationContext());
            //t.Start(TaskScheduler.Default);
        }
    }
}
