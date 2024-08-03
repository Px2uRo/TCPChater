using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCPChatterAllPlatforms.MyNet
{
    public class WindowsIPProvider : IIPProvider
    {
        public IEnumerable<string> GetIPs()
        {
            var list = new List<string>();
            var ary = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in ary)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) // 只获取 IPv4 地址
                {
                    var unHandled = ip.ToString();
                    var res = "";
                    var ctr = 0;
                    for (int i = 0; i < unHandled.Length; i++)
                    {
                        res+= unHandled[i].ToString();
                        if (unHandled[i].ToString()==".")
                        {
                            ctr++;
                            if (ctr == 3)
                            {
                                list.Add( res);
                            }
                        }
                    }
                }
            }
            return list;
        }
    }

    public class AndroidIPProvider : IIPProvider
    {
        public IEnumerable<string> GetIPs()
        {
            throw new NotImplementedException();
        }
    }

    public class LinuxIPProvider : IIPProvider
    {
        public IEnumerable<string> GetIPs()
        {
            throw new NotImplementedException();
        }
    }

    public interface IIPProvider
    {
        public IEnumerable< string> GetIPs();
    }
}
