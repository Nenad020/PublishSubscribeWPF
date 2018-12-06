using Common.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string timeDate = DateTime.Now.ToString();

            Test(timeDate, 10, 1);
            Test(timeDate, 100, 1);
            Test(timeDate, 1000, 1);
            Test(timeDate, 10000, 1);
            Test(timeDate, 100000, 1);
            Test(timeDate, 1000000, 1);
            Test(timeDate, 10000000, 1);
            Console.WriteLine("DONE CONTAINS METHOD\n");

            Test(timeDate, 10, 2);
            Test(timeDate, 100, 2);
            Test(timeDate, 1000, 2);
            Test(timeDate, 10000, 2);
            Test(timeDate, 100000, 2);
            Test(timeDate, 1000000, 2);
            Test(timeDate, 10000000, 2);
            Console.WriteLine("DONE FIRST/TRYGETVALUE METHOD\n");

            Test(timeDate, 10, 3);
            Test(timeDate, 100, 3);
            Test(timeDate, 1000, 3);
            Test(timeDate, 10000, 3);
            Test(timeDate, 100000, 3);
            Test(timeDate, 1000000, 3);
            Test(timeDate, 10000000, 3);
            Console.WriteLine("DONE FOREACH METHOD\n");

            Console.ReadLine();
        }

        public static void Test(string timeDate, int howMuch, int option)
        {
            Stopwatch sw = new Stopwatch();
            Random rand = new Random();

            List<SwitchDevice> listSwitch = new List<SwitchDevice>(howMuch);
            HashSet<SwitchDevice> hashSwitch = new HashSet<SwitchDevice>();
            Dictionary<int, SwitchDevice> dictSwitch = new Dictionary<int, SwitchDevice>(howMuch);

            HashSet<int> switchHashIds = new HashSet<int>();

            SwitchDevice switchDevice;
            for (int i = 0; i < howMuch; i++)
            {
                while (true)
                {
                    int id = rand.Next(0, howMuch*10);
                    switchDevice = new SwitchDevice(id, 0, timeDate);

                    if (switchHashIds.Contains(id))
                        continue;
                    else
                    {
                        listSwitch.Add(switchDevice);
                        dictSwitch.Add(id, switchDevice);
                        hashSwitch.Add(switchDevice);
                        switchHashIds.Add(id);
                        break;
                    }
                }
            }

            var switchListIds = listSwitch.Select(x => x.SwitchID).ToList();

            //number from the middle of list
            int number = switchListIds[howMuch / 2];

            //Output in milliseconds for each List, HashSet and Dictionary
            long switchListMili = 0;
            long switchListHash = 0;
            long switchListDict = 0;

            if (option == 1)
            {
                sw.Start();
                switchListIds.Contains(number);
                sw.Stop();

                switchListMili = sw.ElapsedMilliseconds;

                sw.Restart();
                switchHashIds.Contains(number);
                sw.Stop();

                switchListHash = sw.ElapsedMilliseconds;

                sw.Restart();
                dictSwitch.ContainsKey(number);
                sw.Stop();

                switchListDict = sw.ElapsedMilliseconds;
            }
            else if (option == 2)
            {
                sw.Start();
                var listValue = listSwitch.First(x => x.SwitchID == number);
                sw.Stop();

                switchListMili = sw.ElapsedMilliseconds;

                sw.Restart();
                var hashValue = hashSwitch.First(x => x.SwitchID == number);
                sw.Stop();

                switchListHash = sw.ElapsedMilliseconds;

                SwitchDevice dictValue = null;
                sw.Restart();
                dictSwitch.TryGetValue(number, out dictValue);
                sw.Stop();

                switchListDict = sw.ElapsedMilliseconds;
            }
            else
            {
                sw.Start();
                foreach (var item in listSwitch)
                {

                }
                sw.Stop();

                switchListMili = sw.ElapsedMilliseconds;

                sw.Restart();
                foreach (var item in hashSwitch)
                {

                }
                sw.Stop();

                switchListHash = sw.ElapsedMilliseconds;

                sw.Restart();
                foreach (var item in dictSwitch)
                {

                }
                sw.Stop();

                switchListDict = sw.ElapsedMilliseconds;
            }

            Console.WriteLine("List with {0} elements: {1} in milliseconds", howMuch, switchListMili);
            Console.WriteLine("Hashset with {0} elements: {1} in milliseconds", howMuch, switchListHash.ToString());
            Console.WriteLine("Dictionary with {0} elements: {1} in milliseconds", howMuch, switchListDict.ToString());
            Console.WriteLine();
        }
    }
}
