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
    public class CpuPageViewModel : BaseViewModel
    {
        private ObservableCollection<Info> _info;        
        public ObservableCollection<Info> Info
        {
            get { return _info; }
            set { _info = value; RaisePropertyChanged(() => Info); }
        }

        public CpuPageViewModel(Action<object> onLoadAction)
        {
            LoadData(onLoadAction);
        }

        private void LoadData(Action<object> onLoadAction)
        {
            var info = new List<Info>();
            Task.Factory.StartNew(() =>
            {
                info.Add(new Info() { Description = "Cpu", Value = CpuUtil.GetCPUInfo() });
                info.Add(new Info() { Description = "Stepping", Value = CpuUtil.GetCpuCaption() });
                info.Add(new Info() { Description = "Aktuell Mhz", Value = CpuUtil.GetCpuClockSpeed().ToString() });
                info.Add(new Info() { Description = "Antal kärnor", Value = CpuUtil.GetCpuCores().ToString() });
                info.Add(new Info() { Description = "Trådar", Value = CpuUtil.GetCpuNumberOfLogicalProcessors().ToString() });
                info.Add(new Info() { Description = "Minne", Value = CpuUtil.GetInstalledMemory().ToString() });                
                info.Add(new Info() { Description = "Bitar", Value = CpuUtil.GetCpuDataWidth().ToString() +"Bit" });                
                info.Add(new Info() { Description = "Drivspänning", Value = CpuUtil.GetCpuVoltage().ToString() +"V" });
 
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).ContinueWith((task =>
            {
                Info = new ObservableCollection<Info>(info);
                onLoadAction.Invoke(null);

            }), TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
