using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

namespace TFG.Dialog {
    public class Dialog {
        int _line;                      // Error handling purpuses
        string _text;
        string[] _generalAttributes;
        string[] _p1Attributes;
        string[] _p2Attributes;

        public Dialog(string text, string[] generalAttributes, string[] p1Attributes, string[] p2Attributes, int line) {
            _text = text;
            _generalAttributes = generalAttributes;
            _p1Attributes = p1Attributes;
            _p2Attributes = p2Attributes;
            _line = line;
        }

        public int      Line              { get => _line; }
        public string   Text              { get => _text; }
        public string[] P1Attributes      { get => _p1Attributes; }
        public string[] P2Attributes      { get => _p2Attributes; }
        public string[] GeneralAttributes { get => _generalAttributes; }
    }

    public class Parser {
        public static List<Dialog> LoadFileFromPath(string path) {
            List<Dialog> dialogs = new List<Dialog>();

            if (!File.Exists(path)) {
                Debug.LogWarning(path + " File doesn't exists. Aborting...");
                return dialogs;
            }

            using (var reader = new StreamReader(path)) {
                int lineIdx = 0;
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine().Trim();
                    lineIdx++;
                    if (!string.IsNullOrEmpty(line) && !line.StartsWith("//"))
                        dialogs.Add(ParseLine(line, lineIdx));
                }
            }

            return dialogs;
        }

        public static List<Dialog> LoadFileFromString(string txt) {
            List<Dialog> dialogs = new List<Dialog>();

            if (string.IsNullOrEmpty(txt)) {
                Debug.LogWarning("The string doesn't have data. Aborting...");
                return dialogs;
            }

            using (var reader = new StringReader(txt)) {
                int lineIdx = 0;
                string line;
                while ((line = reader.ReadLine()) != null) {
                    lineIdx++;
                    if (!string.IsNullOrEmpty(line) && !line.StartsWith("//"))
                        dialogs.Add(ParseLine(line, lineIdx));
                }
            }

            return dialogs;
        }

        static Dialog ParseLine(string line, int lineIdx) {
            int idx = line.IndexOf(":");

            var (attr, p1, p2) = ParseAttributes(line.Substring(0, idx));
            var txt = line.Substring(idx + 1);

            return new Dialog(txt, attr, p1, p2, lineIdx);
        }

        static (string[], string[], string[]) ParseAttributes(string attributes) {
            char[] trimChars = { '[', ']' };
            string key = attributes.Trim(trimChars);

            var allAttr = new List<string>(key.Split('|'));
            string[] p1 = new string[0];
            string[] p2 = new string[0];

            for (int i = allAttr.Count - 1; i >= 0; i--) {
                var attr = allAttr[i];
                if (!attr.StartsWith("P"))
                    continue;

                var startIdx = attr.IndexOf('(');
                var endIdx = attr.LastIndexOf(')');
                var tempAttr = attr.Substring(startIdx + 1, endIdx - startIdx - 1);
                var pAttr = tempAttr.Split(',');

                if (attr.StartsWith("P1"))
                    p1 = pAttr;
                else
                    p2 = pAttr;
            }

            return (allAttr.ToArray(), p1, p2);
        }
    }
}