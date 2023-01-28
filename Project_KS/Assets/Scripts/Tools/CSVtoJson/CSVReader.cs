using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tools
{
    public static class CSVReader
    {
        static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        static char[] TRIM_CHARS = { '\"' };
        public static List<Dictionary<string, string>> ReadToString(TextAsset data)
        {
            var list = new List<Dictionary<string, string>>();

            var lines = Regex.Split(data.text, LINE_SPLIT_RE);

            if (lines.Length <= 1)
                return list;

            var types = Regex.Split(lines[0], SPLIT_RE);
            var header = Regex.Split(lines[1], SPLIT_RE);
            for (var i = 2; i < lines.Length; i++)
            {

                var values = Regex.Split(lines[i], SPLIT_RE);
                if (values.Length == 0 || values[0] == "")
                    continue;

                var entry = new Dictionary<string, string>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {

                    if (types[j] == "ignore")
                        continue;

                    entry[header[j]] = values[j].TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                }
                list.Add(entry);
            }
            return list;
        }
    }
}