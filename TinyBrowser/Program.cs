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
            //sw.Write(request, 0, request.Length);
            var response = sr.ReadToEnd();
            Console.WriteLine(response);


            /*var title = "<title>acme.com</title>\n<title>github.com</title>\n<title>marc.com</title>\n<title>ReMarcAble.com</title>";
            List<int> titleTags = title.AllIndexesOf(Constants.titleTag);
            List<int> endTitleTags = title.AllIndexesOf(Constants.endTitleTag); 
            List<string> titles = titleTags.GetTitle(Constants.stepForward, endTitleTags, title); 
            foreach (var s in titles) {
                Console.WriteLine(s);
            }*/
        }
    }
    public static class Constants {
        public static string titleTag = "<title>"; 
        public static string endTitleTag = "</title>"; 
        public static int stepForward = 7; 
    }
}
/*
var string = "";
int first = title.IndexOf('s') + 1; // first
int last = title.LastIndexOf('d'); // last
var text = title[first..last]; // get range
*/
