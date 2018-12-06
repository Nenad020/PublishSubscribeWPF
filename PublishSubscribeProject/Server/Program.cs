using Common.Base;
using Common.DAO;
using Common.Model;
using Common.WCFService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            AddSubscriber addSub = new AddSubscriber();
            addSub.AddSwitchesInDict();

            InitializeSubscribeService(addSub);

            Console.WriteLine("Press ENTER to start randomize");
            Console.ReadKey(true);
            Console.WriteLine("You have started randomizing");
            StartRandom(addSub);

            Console.ReadLine();
        }

        private static void InitializeSubscribeService(AddSubscriber addSub)
        {
            try
            {
                string address = "net.tcp://localhost:4000/IAddSubscriber";
                ServiceHost host = new ServiceHost(addSub);

                host.AddServiceEndpoint(typeof(IAddSubscriber), new NetTcpBinding(), new Uri(address));
                host.Open();

                Console.WriteLine("Server open on address: {0}", address);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void StartRandom(AddSubscriber addSub)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            SwitchDevice switchDevice = null;

            while (true)
            {
                addSub.ChangeSwitch(rand, switchDevice);
            }
        }
    }
}
