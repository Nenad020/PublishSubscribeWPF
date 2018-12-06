using Common.Comperer;
using Common.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base
{
    public class BaseAccess
    {
        private static string OpenConnection(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static Dictionary<int, SwitchDevice> Load()
        {
            using (IDbConnection cnn = new SQLiteConnection(OpenConnection()))
            {
                var output = cnn.Query<SwitchDevice>("select * from Switch", new DynamicParameters());
                List<SwitchDevice> switchesList = output.ToList();
                
                Dictionary<int, SwitchDevice> switchesOutputDict = new Dictionary<int, SwitchDevice>(switchesList.Count);

                foreach (SwitchDevice item in switchesList)
                {
                    SwitchDevice temp = null;
                    if (!switchesOutputDict.TryGetValue(item.SwitchID, out temp) || DateTime.Parse(item.SwitchDate) > DateTime.Parse(temp.SwitchDate))
                    {
                        switchesOutputDict[item.SwitchID] = item;
                    }
                }

                return switchesOutputDict;
            }
        }

        public static void Save(SwitchDevice switchDevice)
        {
            using (IDbConnection cnn = new SQLiteConnection(OpenConnection()))
            {
                cnn.Execute("insert into Switch (SwitchID, SwitchValue, SwitchDate) values (@SwitchID, @SwitchValue, @SwitchDate)", switchDevice);
            }
        }
    }
}
