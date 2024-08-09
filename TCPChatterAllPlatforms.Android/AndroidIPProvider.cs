using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using System.Threading.Tasks;
using Toast = Android.Widget.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCPChatterAllPlatforms.MyNet;
using Android.Text.Format;
using Android.Views;

namespace TCPChatterAllPlatforms.Android
{
    public class AndroidIPProvider : IIPProvider
    {
        private Activity _activity;
        public AndroidIPProvider(Activity activity)
        {
                _activity = activity;
        }
        public IEnumerable<string> GetIPs()
        {
            var list = new List<string>();
            try
            {
                WifiManager wifiMgr = (WifiManager)_activity.GetSystemService(Context.WifiService);
                WifiInfo wifiInfo = wifiMgr.ConnectionInfo;
                int ip = wifiInfo.IpAddress;
                var unHandled = Formatter.FormatIpAddress(ip);
                var res = "";
                var ctr = 0;
                for (int i = 0; i < unHandled.Length; i++)
                {
                    res += unHandled[i].ToString();
                    if (unHandled[i].ToString() == ".")
                    {
                        ctr++;
                        if (ctr == 3)
                        {
                            list.Add(res);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return list;
        }
    }

}
