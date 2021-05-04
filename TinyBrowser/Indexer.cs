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
        public static List<string> GetString(this List<int> tags, int stepForward, List<int> endTags, string str) {
            if (tags == null || endTags == null)
                throw new ArgumentException("the list can not be empty:", $"TitleTags.Count = {tags.Count}");
            return (from position in tags let first = position + stepForward let last = endTags[tags.IndexOf(position)] select str[first..last]).ToList();
        }
    }
}