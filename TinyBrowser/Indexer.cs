using System;
using System.Collections.Generic;
using System.Linq;

namespace Extender {
    public static class Indexer {
        public static List<int> AllIndexesOf(this string str, string value) {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length) {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
        public static List<string> GetTitle(this List<int> titleTags, int stepForward, List<int> endTitleTags, string title) {
            if (titleTags == null)
                throw new ArgumentException("the list can not be empty:", $"TitleTags.Count = {titleTags.Count}");
            return (from position in titleTags let first = position + stepForward let last = endTitleTags[titleTags.IndexOf(position)] select title[first..last]).ToList();
        }
    }
}