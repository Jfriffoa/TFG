using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

namespace TFG.Dialog {
    public class DialogueManager : MonoBehaviour {
        [Header("Source file")]
        public TextAsset rawFile;
        
        [Header("Load Options")]
        public bool loadOnStart = true;
        public bool beginOnLoad;

        [Header("UI")]
        public bool useTMP;
        public Text text;
        public TextMeshProUGUI tmp;

        public Portrait P1;
        public Portrait P2;

        [Header("Callbacks")]
        public UnityEvent onLoad;
        public UnityEvent onStart;
        public UnityEvent onNextDialog;
        public UnityEvent onFinish;

        // Dialogue private variables
        List<Dialog> _dialogs;
        int _dialogIndex = 0;
        
        // Start is called before the first frame update
        void Start() {
            if (loadOnStart)
                Load();
        }
        
        public void Load() {
            StartCoroutine(LoadFile());
        }
        
        IEnumerator LoadFile() {
            _dialogs = Parser.LoadFileFromString(rawFile.text);
            
            yield return null;
            
            onLoad.Invoke();
            if (beginOnLoad)
                Begin();
        }

        void SetText(string newText) {
            if (useTMP)
                tmp.text = newText;
            else
                text.text = newText;
        }
        
        public void Begin() {
            _dialogIndex = 0;
            SetText(_dialogs[_dialogIndex].Text);
            onStart.Invoke();
            UpdateAttributes();
        }

        public void Next() {
            _dialogIndex++;
            if (_dialogIndex >= _dialogs.Count) {
                Debug.Log("Dialog reached to an end.");
                onFinish.Invoke();
            } else {
                SetText(_dialogs[_dialogIndex].Text);
                UpdateAttributes();
                onNextDialog.Invoke();
            }
        }

        void UpdateAttributes() {
            var dialog = _dialogs[_dialogIndex];

            // TODO: Any general attribute to manage
            switch (dialog.GeneralAttributes) {
                default: break;
            }

            if (P1 != null) {
                P1.ApplyAttributes(dialog.P1Attributes, dialog.Line);
            }

            if (P2 != null) {
                P2.ApplyAttributes(dialog.P2Attributes, dialog.Line);
            }
        }
    }
}