using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAO
{
    public interface ISwitchDeviceDAO
    {
        Dictionary<int, SwitchDevice> GetAllSwitches();
        void SaveSwitch(SwitchDevice switchDevice);
    }
}
