using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class SwitchDevice
    {
        int switchID;
        int switchValue;
        string switchDate;

        public SwitchDevice()
        {

        }

        public SwitchDevice(int id, int value, string date)
        {
            SwitchID = id;
            SwitchValue = value;
            SwitchDate = date;
        }

        public SwitchDevice(SwitchDevice orig)
        {
            SwitchID = orig.SwitchID;
            SwitchValue = orig.SwitchValue;
            SwitchDate = orig.SwitchDate;
        }

        [DataMember]
        public int SwitchID
        {
            get { return switchID; }
            set { switchID = value; }
        }

        [DataMember]
        public int SwitchValue
        {
            get { return switchValue; }
            set { switchValue = value; }
        }

        [DataMember]
        public string SwitchDate
        {
            get { return switchDate; }
            set { switchDate = value; }
        }

        public override bool Equals(object obj)
        {
            SwitchDevice sd = obj as SwitchDevice;

            if (sd == null)
            {
                return false;
            }

            return this.switchID == sd.switchID;
        }
    }
}
