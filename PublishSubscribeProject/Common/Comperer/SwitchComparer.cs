using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Comperer
{
    public class SwitchComparer : IComparer<SwitchDevice>
    {
        public int Compare(SwitchDevice x, SwitchDevice y)
        {
            return x.SwitchID - y.SwitchID;
        }
    }
}
