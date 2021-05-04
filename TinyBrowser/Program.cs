using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Extender;

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
            //Console.WriteLine(response);
            int first = response.IndexOf(Constants.titleTag) + Constants.stepForward;
            int last = response.LastIndexOf(Constants.endTitleTag);
            var text = response[first..last];
            Console.WriteLine(text);
            
        }
    }
    public static class Constants {
        public static string titleTag = "<title>"; 
        public static string endTitleTag = "</title>";
        public static int stepForward = 7; 
    }
}
