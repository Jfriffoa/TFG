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

        [Header("UI")]
        public bool useTMP;
        public Text text;
        public TextMeshProUGUI tmp;

        public GameObject P1;
        public GameObject P2;

        [Header("Callbacks")]
        public UnityEvent onStart;
        public UnityEvent onNextDialog;
        public UnityEvent onFinish;

        // Dialogue private variables
        List<Dialog> _dialogs;
        int _dialogIndex = 0;
        
        // Start is called before the first frame update
        IEnumerator Start() {
            if (P1 != null) P1.SetActive(false);
            if (P2 != null) P2.SetActive(false);

            _dialogs = Parser.LoadFileFromString(rawFile.text);
            yield return null;
            OnLoad();
        }

        void SetText(string newText) {
            if (useTMP)
                tmp.text = newText;
            else
                text.text = newText;
        }

        void OnLoad() {
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
            if (P1 == null || P2 == null)
                return;

            switch (_dialogs[_dialogIndex].Attribute) {
                case Dialog.DialogAttribute.P1:
                    P1.SetActive(true);
                    P2.SetActive(false);
                    break;
                case Dialog.DialogAttribute.P2:
                    P1.SetActive(false);
                    P2.SetActive(true);
                    break;
                case Dialog.DialogAttribute.None:
                    P1.SetActive(false);
                    P2.SetActive(false);
                    break;
                case Dialog.DialogAttribute.Both:
                    P1.SetActive(true);
                    P2.SetActive(true);
                    break;
            }
        }
    }
}