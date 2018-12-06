using Common.Model;
using Common.WCFService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<SwitchDevice> switches;
        private static Dictionary<int, SwitchDevice> dictSwitches;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public MainWindowViewModel()
        {
            Switches = dictSwitches.Values.ToList();
        }

        public MainWindowViewModel(IAddSubscriber proxy, string address)
        {
            dictSwitches = new Dictionary<int, SwitchDevice>();
            dictSwitches = proxy.Subscribe(address);

            Switches = dictSwitches.Values.ToList();
        }

        public List<SwitchDevice> Switches
        {
            get { return switches; }
            set
            {
                switches = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Switches"));
            }
        }

        public static bool CheckIfExists(SwitchDevice switchDevice)
        {
            if (dictSwitches.ContainsKey(switchDevice.SwitchID))
            {
                dictSwitches[switchDevice.SwitchID].SwitchValue = switchDevice.SwitchValue;
                dictSwitches[switchDevice.SwitchID].SwitchDate = switchDevice.SwitchDate;

                return true;
            }
            else
                return false;
        }
    }
}
