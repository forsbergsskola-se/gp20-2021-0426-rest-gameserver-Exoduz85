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
            Console.WriteLine(response);
            /*List<int> titleTags = response.AllIndexesOf(Constants.titleTag);
            List<int> endTitleTags = response.AllIndexesOf(Constants.endTitleTag); 
            List<string> titles = titleTags.GetString(Constants.stepForward, endTitleTags, response); 
            foreach (var s in titles) {
                Console.WriteLine(s);
            }*/
            List<int> htmlTags = response.AllIndexesOf(Constants.htmlTag);
            List<int> endHtmlTags = response.AllIndexesOf(Constants.endHtmlTag);
            List<string> htmls = htmlTags.GetString(9, endHtmlTags, response);
            foreach (var html in htmls) {
                Console.WriteLine(html);
            }
        }
    }
    public static class Constants {
        public static string titleTag = "<title>"; 
        public static string endTitleTag = "</title>"; 
        public static string htmlTag = "<a href=\""; 
        public static string endHtmlTag = "\">";
        public static int stepForward = 7; 
    }
}
/*
var string = "";
int first = title.IndexOf('s') + 1; // first
int last = title.LastIndexOf('d'); // last
var text = title[first..last]; // get range
*/
