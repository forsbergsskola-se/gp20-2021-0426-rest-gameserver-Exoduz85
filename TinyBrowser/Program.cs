using System;
using System.Collections.Generic;
using Extender;

namespace gp20_2021_0426_rest_gameserver_Exoduz85 {
    class Program { 
        static void Main(string[] args)
        {
            // String of titles returned from site
            var title = "<title>acme.com</title>\n<title>github.com</title>\n<title>marc.com</title>\n<title>ReMarcAble.com</title>";
            // Get start index of each title
            List<int> titleTags = Indexer.AllIndexesOf(title, Constants.titleTag); 
            // Get end index of each title
            List<int> endTitleTags = Indexer.AllIndexesOf(title, Constants.endTitleTag); 
            // Get all titles from indexes
            List<string> titles = Indexer.GetTitle(titleTags, Constants.stepForward, endTitleTags, title); 
            // Print out all indexes that where found.
            foreach (var s in titles) {
                Console.WriteLine(s);
            }
        }
    }
    public static class Constants {
        // Start title tag
        public static string titleTag = "<title>"; 
        // End title tag
        public static string endTitleTag = "</title>"; 
        // Number of steps to pass "<title>" characters
        public static int stepForward = 7; 
    }
}

/*int first = title.IndexOf("<title>") + 7; // first occurence
int last = title.LastIndexOf("</title>"); // last occurence
var text = title[first..last]; // get range
Console.WriteLine($"first: {first}, last: {last}");
Console.WriteLine(text);*/
