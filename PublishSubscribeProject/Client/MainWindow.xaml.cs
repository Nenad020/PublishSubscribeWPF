using Client.ViewModel;
using Common.WCFService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow main;

        private ChannelFactory<IAddSubscriber> factory = null;
        private IAddSubscriber proxy = null;
        private string address; 

        public MainWindow()
        {
            Thread.Sleep(1000);
            InitializeComponent();

            SetIcon();
            if (StartListener())
                this.Close();

            CollectData();

            main = this;
            this.DataContext = new MainWindowViewModel(proxy, address);
        }

        private void CollectData()
        {
            factory = new ChannelFactory<IAddSubscriber>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:4000/IAddSubscriber"));
            proxy = factory.CreateChannel();
        }

        private bool StartListener()
        {
            Random rand = new Random();
            int addressPort = rand.Next(4001, 5000);
            bool goAgain = true;
            
            while(goAgain)
            {
                try
                {
                    address = "net.tcp://localhost:" + addressPort + "/IReciver";
                    ServiceHost host = new ServiceHost(typeof(Reciver));

                    host.AddServiceEndpoint(typeof(IReciver), new NetTcpBinding(), new Uri(address));
                    host.Open();

                    porukaTbBlock.Text = String.Format("Client listen changes on adress: {0}", address);
                    porukaTbBlock.Foreground = Brushes.Green;
                    goAgain = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return true;
                }
            }

            return false;
        }

        private void SetIcon()
        {
            Uri picUri;

            var directoryPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var localPath = new Uri(directoryPath).LocalPath;
            var replacedString = localPath.Replace("Client\\bin\\Debug", "");

            var iconPath = System.IO.Path.Combine(replacedString, "Common\\Images\\On-Off.png");
            picUri = new Uri(iconPath);
            this.Icon = BitmapFrame.Create(picUri);

            var exitButtonPath = System.IO.Path.Combine(replacedString, "Common\\Images\\Exit.png");
            picUri = new Uri(exitButtonPath);
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(exitButtonPath));
            exitBtn.Background = ib;
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IAddSubscriber> factory = new ChannelFactory<IAddSubscriber>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:4000/IAddSubscriber"));
            IAddSubscriber proxy = factory.CreateChannel();

            proxy.UnSubscribe(address);
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ChannelFactory<IAddSubscriber> factory = new ChannelFactory<IAddSubscriber>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:4000/IAddSubscriber"));
            IAddSubscriber proxy = factory.CreateChannel();

            proxy.UnSubscribe(address);
        }
    }
}
