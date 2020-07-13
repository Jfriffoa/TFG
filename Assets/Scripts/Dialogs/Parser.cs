using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

namespace TFG.Dialog {
    public class Dialog {
        public enum DialogAttribute {
            None, P1, P2, Both
        }

        DialogAttribute _attribute;
        string _text;

        public Dialog(DialogAttribute attribute, string text) {
            _attribute = attribute;
            _text = text;
        }

        public DialogAttribute Attribute { get => _attribute; }
        public string          Text      { get => _text; }
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

            var attr = ParseAttributes(line.Substring(0, idx));
            var txt = line.Substring(idx + 1);

            return new Dialog(attr, txt);
        }

        static Dialog.DialogAttribute ParseAttributes(string attributes) {
            char[] trimChars = { '[', ']' };
            string key = attributes.Trim(trimChars);

            var attr = Dialog.DialogAttribute.None;

            switch (key) {
                case "P1": attr = Dialog.DialogAttribute.P1; break;
                case "P2": attr = Dialog.DialogAttribute.P2; break;
                case "P3": attr = Dialog.DialogAttribute.Both; break;
            }

            return attr;
        }
    }
}