using System.Collections;

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace TFG.Questionaries {
    public class PopulateDropdown : MonoBehaviour {
        public TMP_Dropdown dropdown;
        public TextAsset values;
        
        void Start() {
            dropdown.ClearOptions();

            //Read options
            List<string> options = new List<string>(values.text.Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
            dropdown.AddOptions(options);
        }

    }
}