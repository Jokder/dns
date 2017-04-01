using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Monads;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace magic_dns.Server
{
    public static class WhiteList
    {
        private static readonly string WhiteListPath = AppDomain.CurrentDomain.BaseDirectory + "whitelist.json";
        private static string[] whiteList;
        public static void Write(byte[] data)
        {
            if (!File.Exists(WhiteListPath))
            {
                var dir = Path.GetDirectoryName(WhiteListPath);
                if (!Directory.Exists(dir)) dir.Do(x => Directory.CreateDirectory(x));
            }
            else
            {
                File.Delete(WhiteListPath);
            }
            File.WriteAllBytes(WhiteListPath, data);
        }

        public static string[] Read()
        {
            if (whiteList == null)
            {
                if (!File.Exists(WhiteListPath)) return new string[] { };
                var wltxt = File.ReadAllText(WhiteListPath, Encoding.UTF8);
                whiteList = JsonConvert.DeserializeObject<string[]>(wltxt);
            }
            return whiteList;
        }
    }
}
