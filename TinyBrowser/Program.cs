using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace gp20_2021_0426_rest_gameserver_Exoduz85 {
    class Program {
        const int port = 80;
        const string hostUrl = "acme.com";
        static readonly string request = $"GET / HTTP/1.1\r\nHost: {hostUrl}\r\n\r\n";
        static StreamReader sr;
        static StreamWriter sw;
        static NetworkStream stream;
        static TcpClient client;
        static bool shouldRun = false;
        static void Main(string[] args) {

            //shouldRun = true;

            while (shouldRun) {
                
            }
            
            client = new TcpClient(hostUrl, port);
            stream = client.GetStream();
            sr = new StreamReader(stream);
            var encoding = Encoding.ASCII.GetBytes(request);
            stream.Write(encoding, 0, encoding.Length);
            var response = sr.ReadToEnd();
            var header = ExtractHeader(response);
            var links = ExtractLinks(response);
            Console.WriteLine(header);
            for (var index = 0; index < links.Count; index++) {
                var str = links[index];
                Console.WriteLine($"{index}: {str[0]} ({str[1]})");
            }

            client.Close();
        }
        public static List<string[]> ExtractLinks(string pageResponce) {
            List<string[]> hyperLinks = new List<string[]>();
            var regex = new Regex("<a href=[\"|'](?<link>.*?)[\"|'].*?>(<b>|<img.*?>)?(?<name>.*?)(</b>)?</a>", 
                RegexOptions.IgnoreCase);
            if (!regex.IsMatch(pageResponce)) return hyperLinks;
            foreach(Match match in regex.Matches(pageResponce))
                hyperLinks.Add(new []{match.Groups["name"].Value, match.Groups["link"].Value});
            return hyperLinks;
        }
        public static string ExtractHeader(string str) {
            int first = str.IndexOf("<title>", StringComparison.OrdinalIgnoreCase) + 7;
            int last = str.LastIndexOf("</title>", StringComparison.OrdinalIgnoreCase);
            return str[first..last];
        }
    }
}