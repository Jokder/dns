using System;
using System.IO;
using System.Net;
using System.Text;
using DNS.Protocol.Utils;
using DNS.Server;
using magic_dns.Config;
using magic_dns.Server;
using Newtonsoft.Json;

namespace magic_dns
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("downloading whitelist...");
            DownloadWhiteList();
            Console.WriteLine("download whitelist complete");
            DnsServer server = new DnsServer(new Configer());
            Console.WriteLine("launch dns server.");
            server.Requested += Console.WriteLine;
            server.Responded += (request, response) => Console.WriteLine("{0} => {1}", request, response);
            server.Listen();
        }

        private static void DownloadWhiteList()
        {
            try
            {
                var url = new Configer().WhiteListServer;
                WebClient client = new WebClient();
                var listData = client.DownloadData(url);
                WhiteList.Write(listData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("download whitelist failed:" + ex.Message);
            }
        }
    }
}
