using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using magic_dns.Client;

namespace magic_dns.Config
{
    public class Configer
    {
        public string IspDnsServer => Helper.AutoReadValue("114.114.114.114");
        public string OtherDnsServer => Helper.AutoReadValue("8.8.8.8");
        public string GlobalDnsServer => Helper.AutoReadValue("8.8.8.8");
        public string WhiteListServer => Helper.AutoReadValue("http://chinadomains.info/whitelist/getall");
        public int Port => Helper.AutoReadValue(53);
    }
}
