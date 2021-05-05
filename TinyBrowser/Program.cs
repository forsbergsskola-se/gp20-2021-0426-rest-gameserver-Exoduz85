using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace gp20_2021_0426_rest_gameserver_Exoduz85 {
    class Program {
        const int port = 80;
        const string hostUrl = "acme.com";
        static readonly string request = $"GET / HTTP/1.1\r\nHost: {hostUrl}\r\n\r\n";
        static StreamReader sr;
        static StreamWriter sw;
        static NetworkStream stream;
        static TcpClient client;
        static void Main(string[] args) {

            client = new TcpClient(hostUrl, port);
            stream = client.GetStream();
            sw = new StreamWriter(stream);
            sr = new StreamReader(stream);
            var encoding = Encoding.ASCII.GetBytes(request);
            stream.Write(encoding, 0, encoding.Length);
            var response = sr.ReadToEnd();
            var header = ExtractHeader(response);
            
            Console.WriteLine(header);
            
            client.Close();
        }
        public static string ExtractHeader(string str) {
            int first = str.IndexOf("<title>", StringComparison.OrdinalIgnoreCase) + 7;
            int last = str.LastIndexOf("</title>", StringComparison.OrdinalIgnoreCase);
            return str[first..last];
        }
    }
}