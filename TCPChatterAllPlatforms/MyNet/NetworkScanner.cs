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
        public static IEnumerable<string> ScanNetwork()
        {
            IEnumerable<string> baseips = null;
            if (App.Current.ApplicationLifetime is ISingleViewApplicationLifetime)
            {
                baseips = new AndroidIPProvider().GetIPs();
            }
            else
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    baseips = new WindowsIPProvider().GetIPs();
                }
                else
                {

                    baseips = new LinuxIPProvider().GetIPs();
                }
            }
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
