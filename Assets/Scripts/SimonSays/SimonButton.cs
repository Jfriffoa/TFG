using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TFG.SimonSays {
    public class SimonButton : MonoBehaviour {
        public Color simonColor;
        internal UnityAction onPressed;

        public UnityEvent onPlay;

        Image _image;
        Button _btn;
        bool _interactable = true;

        // Start is called before the first frame update
        void Start() {
            _image = GetComponent<Image>();
            _btn = GetComponent<Button>();
            
            // Add the listener on click
            _btn.onClick.AddListener(() => {
                if (_interactable)
                    onPressed.Invoke();
            });

            // Make the pressed color the same as the simon color
            var colors = _btn.colors;
            colors.pressedColor = simonColor;
            _btn.colors = colors;
        }

        internal void SetButtonEnabled(bool enable) {
            _btn.interactable = enable;
        }

        internal void Win(Color winColor) {
            _btn.interactable = false;
            _image.color = winColor;
        }

        public void Blink(float seconds) {
            if (!_interactable)
                return;

            StartCoroutine(Blinking(seconds, simonColor));
        }

        public void Blink(float seconds, Color color) {
            if (!_interactable)
                return;
            StartCoroutine(Blinking(seconds, color));
        }

        IEnumerator Blinking(float seconds, Color newColor) {
            _interactable = false;
            Color originalColor = _image.color;
            _image.color = newColor;
            onPlay.Invoke();
            yield return new WaitForSeconds(seconds);
            _image.color = originalColor;
            _interactable = true;
        }
    }
}