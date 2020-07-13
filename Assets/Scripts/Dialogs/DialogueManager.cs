using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TFG.Dialog {
    public class DialogueManager : MonoBehaviour {
        public TextAsset rawFile;
        public Text text;

        public GameObject P1;
        public GameObject P2;

        List<Dialog> _dialogs;
        int _dialogIndex = 0;

        [Header("Callbacks")]
        public UnityEvent onStart;
        public UnityEvent onNextDialog;
        public UnityEvent onFinish;
        
        // Start is called before the first frame update
        IEnumerator Start() {
            P1.SetActive(false);
            P2.SetActive(false);

            _dialogs = Parser.LoadFileFromString(rawFile.text);
            yield return null;
            OnLoad();
        }

        void OnLoad() {
            text.text = _dialogs[_dialogIndex].Text;
            onStart.Invoke();
            UpdateAttributes();
        }

        public void Next() {
            _dialogIndex++;
            if (_dialogIndex >= _dialogs.Count) {
                Debug.Log("Dialog reached to an end.");
                onFinish.Invoke();
            } else {
                text.text = _dialogs[_dialogIndex].Text;
                UpdateAttributes();
                onNextDialog.Invoke();
            }
        }

        void UpdateAttributes() {
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