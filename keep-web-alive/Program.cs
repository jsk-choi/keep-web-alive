using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Configuration;

namespace keep_web_alive
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebClient client = new WebClient())
            {
                string urls = ConfigurationManager.AppSettings["urls"];

                foreach (var url in urls.Split(','))
                {
                    string htmlCode = client.DownloadString("https://www.google.com");
                    Console.WriteLine($"did - {url}");
                }
            }
        }
    }
}
