using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TCPChatterAllPlatforms.MyNet;

namespace TCPChatterAllPlatforms
{
    public class NetworkScanner
    {
        public static IEnumerable<string> ScanNetCards()=>App.IPProvdider.GetIPs();

        public IEnumerable<string> GetAvaliableServers(IEnumerable<string> baseips)
        {

            var ret = new List<string>();
            foreach (var item in baseips)
            {
                for (int i = 1; i < 255; i++)
                {
                    string ip = $"{item}{i}";
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(ip, 100);

                    if (reply.Status == IPStatus.Success)
                    {
                        ret.Add(ip);
                    }
                }

            }
            return ret;
        }
    }

}
