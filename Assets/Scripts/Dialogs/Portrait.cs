using UnityEngine;
using UnityEngine.UI;

namespace TFG.Dialog {
    public class Portrait : MonoBehaviour {

        [Header("Portrait variables")]
        public Image portrait;
        Animator _animator;

        public Sprite[] sprites;

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
                    if (attr.StartsWith("img")) {
                    if (int.TryParse(attr.Substring(3), out int idx)) {
                        idx--;
                        if (idx < sprites.Length) {
                            portrait.sprite = sprites[idx];
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
                        case "on":
                        case "off":
                            _animator.SetTrigger(attr);
                            break;
                    }
                }
            }
        }
    }
}