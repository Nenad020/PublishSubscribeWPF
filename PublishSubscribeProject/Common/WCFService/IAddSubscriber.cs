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
    public interface IAddSubscriber
    {
        [OperationContract]
        Dictionary<int, SwitchDevice> Subscribe(string address);

        [OperationContract]
        void UnSubscribe(string address);
    }
}
