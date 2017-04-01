using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Monads;
using System.Text;
using System.Threading.Tasks;

namespace magic_dns.Client
{
    public static class Helper
    {
        private static readonly string ConfPath = AppDomain.CurrentDomain.BaseDirectory + "app.conf";
        public static TValue ReadValue<TValue>(string key)
        {
            return ReadValue(key, default(TValue));
        }

        public static TValue AutoReadValue<TValue>()
        {
            StackTrace stack = new StackTrace();
            var key = stack.GetFrame(1).GetMethod()?.Name.Substring(4);
            return ReadValue<TValue>(key);
        }

        public static TValue AutoReadValue<TValue>(TValue defaultValue)
        {
            StackTrace stack = new StackTrace();
            var key = stack.GetFrame(1).GetMethod()?.Name.Substring(4);
            return ReadValue(key, defaultValue);
        }

        public static TValue ReadValue<TValue>(string key, TValue defaultValue)
        {
            var valType = typeof(TValue);
            try
            {
                var sections = ReadAllSections();
                if (sections == null) return default(TValue);
                if (!sections.ContainsKey(key))
                {
                    if (!Equals(defaultValue, default(TValue)))
                    {
                        sections.Add(key, defaultValue.ToString());
                        WriteConfig(sections);
                        return defaultValue;
                    }
                }
                var value = sections[key];
                if (value.StartsWith("$"))
                {
                    value = sections[value.Substring(1)];
                }
                return (TValue)Convert.ChangeType(value, valType);
            }
#if DEBUG
            catch (Exception ex)
            {
                return default(TValue);
            }
#else
            catch
            {
                return default(TValue);
            }
#endif
        }

        private static void WriteConfig(IDictionary<string, string> sections)
        {
            if (!File.Exists(ConfPath))
            {
                var dir = Path.GetDirectoryName(ConfPath);
                if (!Directory.Exists(dir)) dir.Do(x => Directory.CreateDirectory(x));
            }
            else
            {
                File.Delete(ConfPath);
            }
            var lines = sections.Select(x => x.Key + "=" + x.Value).ToArray();
            File.WriteAllLines(ConfPath, lines);
        }

        private static IDictionary<string, string> ReadAllSections()
        {
            if (!File.Exists(ConfPath)) return new Dictionary<string, string>();
            var allLines = File.ReadAllLines(ConfPath);
            return allLines.Where(x => !x.StartsWith("#"))
                .Select(x => x.Substring(0, x.IndexOf("#", StringComparison.OrdinalIgnoreCase) - 1).Trim())
                .Select(x =>
                {
                    var indexOfEqual = x.IndexOf("=", StringComparison.OrdinalIgnoreCase);
                    return new KeyValuePair<string, string>(x.Substring(0, indexOfEqual),
                        x.Substring(indexOfEqual + 1));
                }).ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.OrdinalIgnoreCase);
        }
    }
}
