using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Configuration;
using System.Diagnostics;

namespace keep_web_alive
{
    class Program
    {
        static void Main(string[] args)
        {
            bool eventLog = Convert.ToBoolean(ConfigurationManager.AppSettings["eventlog"]);
            string urlstr = "";

            using (WebClient client = new WebClient())
            {
                string urls = ConfigurationManager.AppSettings["urls"];

                foreach (var url in urls.Split(','))
                {
                    urlstr = url;

                    try
                    {
                        string htmlCode = client.DownloadString(url);
                        var msg = $"did - {url}";

                        Console.WriteLine(msg);

                        if (eventLog)
                        {
                            using (var evt = new EventLog("Application"))
                            {
                                evt.Source = "Application";
                                evt.WriteEntry(msg, EventLogEntryType.Information, 10001);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var msg = $"failed - {url} - {ex.Message}";

                        Console.WriteLine(msg);
                        using (var evt = new EventLog("Application"))
                        {
                            evt.Source = "Application";
                            evt.WriteEntry(msg, EventLogEntryType.Information, 10001);
                        }
                    }

                }
            }
        }
    }
}
