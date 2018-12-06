using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.WCFService
{
    [ServiceContract]
    public interface IReciver
    {
        [OperationContract]
        void AddNewSwitch(SwitchDevice switchDevice);
    }
}
