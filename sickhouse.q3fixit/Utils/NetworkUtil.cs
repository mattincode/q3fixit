using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace sickhouse.q3fixit.Utils
{
    public class Nic
    {
        public string Name { get; set; }
        public OperationalStatus Status { get; set; }
    }

    public static class NetworkUtil
    {
        public static List<Nic> GetNetworkAdapters()
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            return interfaces.Select(adapter => new Nic() {Name = adapter.Name, Status = adapter.OperationalStatus}).ToList();
        }

        ///<summary>Gets the computer's external IP address from the internet.</summary>
        public static IPAddress GetExternalAddress()
        {
            //<html><head><title>Current IP Check</title></head><body>Current IP Address: 129.98.193.226</body></html>
            var html = new WebClient().DownloadString("http://checkip.dyndns.com/");

            var ipStart = html.IndexOf(": ", StringComparison.OrdinalIgnoreCase) + 2;
            return IPAddress.Parse(html.Substring(ipStart, html.IndexOf("</", ipStart, StringComparison.OrdinalIgnoreCase) - ipStart));
        }


        public static bool IsLocalIpAddress(string host)
        {
            try
            { // get host IP addresses
                IPAddress[] hostIPs = Dns.GetHostAddresses(host);
                // get local IP addresses
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // test if any host IP equals to any local IP or to localhost
                foreach (IPAddress hostIP in hostIPs)
                {
                    // is localhost
                    if (IPAddress.IsLoopback(hostIP)) return true;
                    // is local address
                    foreach (IPAddress localIP in localIPs)
                    {
                        if (hostIP.Equals(localIP)) return true;
                    }
                }
            }
            catch { }
            return false;
        }

        public static string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
 
    }
}
