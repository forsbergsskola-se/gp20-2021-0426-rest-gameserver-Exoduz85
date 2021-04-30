using System;
using System.Collections.Generic;
using Extender;

namespace gp20_2021_0426_rest_gameserver_Exoduz85 {
    class Program { 
        static void Main(string[] args)
        {
            var title = "<title>acme.com</title>\n<title>github.com</title>\n<title>marc.com</title>\n<title>ReMarcAble.com</title>";
            List<int> titleTags = title.AllIndexesOf(Constants.titleTag);
            List<int> endTitleTags = title.AllIndexesOf(Constants.endTitleTag); 
            List<string> titles = titleTags.GetTitle(Constants.stepForward, endTitleTags, title); 
            foreach (var s in titles) {
                Console.WriteLine(s);
            }
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
