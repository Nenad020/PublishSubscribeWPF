using Common.WCFService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Client.ViewModel;
using System.Windows;

namespace Client
{
    public class Reciver : IReciver
    {
        public void AddNewSwitch(SwitchDevice switchDevice)
        {
            bool exits = MainWindowViewModel.CheckIfExists(switchDevice);
            if (exits)
                MainWindow.main.DataContext = new MainWindowViewModel();
            else
            {
                Exception exception = new Exception("Can't add new switch");
                MessageBox.Show(exception.Message);

                return;
            }
        }
    }
}
