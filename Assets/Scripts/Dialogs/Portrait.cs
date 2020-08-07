using UnityEngine;
using UnityEngine.UI;

namespace TFG.Dialog {
    public class Portrait : MonoBehaviour {

        [System.Serializable]
        public struct Container {
            public Sprite sprite;
            public string name;
        }

        [Header("Portrait variables")]
        public Image portrait;
        public Text title;
        Animator _animator;
        bool _locked = false;

        public Container[] sprites;

        void Start() {
            _animator = GetComponentInChildren<Animator>();

            if (portrait == null)
                Debug.LogWarning("Portrait Image not specified. Attributes won't work well.", gameObject);
        }

        internal void ApplyAttributes(string[] attributes, int line) {
            foreach (var attr in attributes) {
                // Handle nulls
                if (string.IsNullOrEmpty(attr))
                    continue;

                // Handle the change of sprite
                if (attr.StartsWith("img") && !_locked) {
                    if (int.TryParse(attr.Substring(3), out int idx)) {
                        if (idx < sprites.Length) {
                            portrait.sprite = sprites[idx].sprite;

                            if (!string.IsNullOrEmpty(sprites[idx].name)){
                                title.text = sprites[idx].name;
                            }
                        } else {
                            Debug.LogError("[img] is trying to access non-exist index: " + idx + " in the line " + line, gameObject);
                        }
                    } else {
                        Debug.LogError("There was an [img] error in line " + line, gameObject);
                    }
                // Handle the animation attributes
                } else {
                    switch (attr) {
                        case "enter":
                        case "exit":
                            _animator.SetTrigger(attr);
                            break;
                        case "on":
                            _animator.SetTrigger(attr);
                            transform.SetAsLastSibling();
                            break;
                        case "off":
                            _animator.SetTrigger(attr);
                            transform.SetAsFirstSibling();
                            break;
                    }
                }
            }
        }
        public void LockSprite() {
            _locked = true;
        }

        public void UnlockSprite() {
            _locked = false;
        }

        public void Hide() {
            _animator.enabled = false;
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public void Show() {
            _animator.enabled = true;
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}