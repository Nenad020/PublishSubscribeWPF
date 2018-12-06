using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Base;

namespace Common.DAO
{
    public class SwitchDeviceDAO : ISwitchDeviceDAO
    {
        public SwitchDeviceDAO()
        {

        }

        public Dictionary<int, SwitchDevice> GetAllSwitches()
        {
            return BaseAccess.Load();
        }

        public void SaveSwitch(SwitchDevice switchDevice)
        {
            BaseAccess.Save(switchDevice);
        }
    }
}
