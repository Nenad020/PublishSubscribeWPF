using Common.WCFService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Base;
using System.ServiceModel;
using Common.DAO;
using System.Windows;
using System.Threading;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AddSubscriber : IAddSubscriber
    {
        private Dictionary<int, SwitchDevice> dictSwitches = null;
        private ISwitchDeviceDAO switchDAO = new SwitchDeviceDAO();
        
        private Dictionary<string, IReciver> proxyDict = new Dictionary<string, IReciver>();

        public void AddSwitchesInDict()
        {
            dictSwitches = switchDAO.GetAllSwitches();
        }

        public Dictionary<int, SwitchDevice> Subscribe(string address)
        {
            ChannelFactory<IReciver> factory = new ChannelFactory<IReciver>(new NetTcpBinding(), new EndpointAddress(address));
            IReciver proxy = factory.CreateChannel();
            proxyDict.Add(address, proxy);

            return dictSwitches;
        }

        public void UnSubscribe(string address)
        {
            proxyDict.Remove(address);
        }

        private void SendSwitch(SwitchDevice switchDevice)
        {
            foreach (var client in proxyDict.ToList())
            {
                try
                {
                    client.Value.AddNewSwitch(switchDevice);
                }
                catch(Exception)
                {
                }
            }
        }

        public void ChangeSwitch(Random rand, SwitchDevice switchDevice)
        {
            int index = rand.Next(0, dictSwitches.Count);
            int idSwitch = dictSwitches.Keys.ToList()[index];

            if (!dictSwitches.ContainsKey(idSwitch))
            {
                Exception exception = new Exception("Can't add new switch");
                MessageBox.Show(exception.Message);

                return;
            }
            else
            {
                dictSwitches[idSwitch].SwitchDate = DateTime.Now.ToString();

                if (dictSwitches[idSwitch].SwitchValue == 0)
                    dictSwitches[idSwitch].SwitchValue = 1;
                else
                    dictSwitches[idSwitch].SwitchValue = 0;
            }

            switchDevice = new SwitchDevice(dictSwitches[idSwitch]);

            Console.WriteLine("ID: {0}, Value: {1}, Date: {2}", switchDevice.SwitchID, switchDevice.SwitchValue, switchDevice.SwitchDate);
            switchDAO.SaveSwitch(switchDevice);
            SendSwitch(switchDevice);

            Thread.Sleep(5000);
        }
    }
}
