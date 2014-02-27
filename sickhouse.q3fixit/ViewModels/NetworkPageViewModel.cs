using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using sickhouse.q3fixit.Utils;

namespace sickhouse.q3fixit.ViewModels
{
    public class NetworkPageViewModel : BaseViewModel
    {
        private ObservableCollection<Info> _networkInfo;

        public ObservableCollection<Info> NetworkInfo
        {
            get { return _networkInfo; }
            set { _networkInfo = value; RaisePropertyChanged(() => NetworkInfo); }
        }

        public NetworkPageViewModel(Action<object> onLoadAction)
        {
            LoadData(onLoadAction);
        }

        private void LoadData(Action<object> onLoadAction)
        {
            var info = new List<Info>();
            Task.Factory.StartNew(() =>
            {
                info.Add(new Info() { Description = "Extern IP-address", Value = NetworkUtil.GetExternalAddress().ToString() });
                info.Add(new Info() { Description = "Intern IP-address", Value = NetworkUtil.LocalIPAddress() });
                info.Add(new Info() { Description = "", Value = "" });    
                info.Add(new Info() { Description = "*** Network adapters ***", Value = "" });    
                var adapters = NetworkUtil.GetNetworkAdapters();
                foreach (var adapter in adapters)
                {
                    info.Add(new Info() { Description = adapter.Name, Value = adapter.Status.ToString() });    
                }
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).ContinueWith((task =>
            {
                NetworkInfo = new ObservableCollection<Info>(info);
                onLoadAction.Invoke(null);

            }), TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
