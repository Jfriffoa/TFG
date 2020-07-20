using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

namespace TFG.Dialog {
    public class Dialog {
        string _text;
        string[] _generalAttributes;
        string[] _p1Attributes;
        string[] _p2Attributes;

        public Dialog(string text, string[] generalAttributes, string[] p1Attributes, string[] p2Attributes) {
            _text = text;
            _generalAttributes = generalAttributes;
            _p1Attributes = p1Attributes;
            _p2Attributes = p2Attributes;
        }

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
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine().Trim();
                    dialogs.Add(ParseLine(line));
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
                string line;
                while ((line = reader.ReadLine()) != null) {
                    dialogs.Add(ParseLine(line));
                }
            }

            return dialogs;
        }

        static Dialog ParseLine(string line) {
            int idx = line.IndexOf(":");

            var (attr, p1, p2) = ParseAttributes(line.Substring(0, idx));
            var txt = line.Substring(idx + 1);

            return new Dialog(txt, attr, p1, p2);
        }

        static (string[], string[], string[]) ParseAttributes(string attributes) {
            char[] trimChars = { '[', ']' };
            string key = attributes.Trim(trimChars);

            var allAttr = new List<string>(attributes.Split('|'));
            string[] p1 = new string[1];
            string[] p2 = new string[1];

            for (int i = allAttr.Count - 1; i >= 0; i--) {
                var attr = allAttr[i];
                if (!attr.StartsWith("P"))
                    continue;

                var startIdx = attr.IndexOf('(');
                var endIdx = attr.LastIndexOf(')');
                var tempAttr = attr.Substring(startIdx + 1, endIdx - startIdx);
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