using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace TFG {
    public class WillingnessController : MonoBehaviour {
        // Singleton cuz yes
        static WillingnessController _instance;
        public static WillingnessController Instance { get => _instance; }

        [Range(-1, 1)]
        public float willingness = 0;

        [Header("UI")]
        public RectTransform sliderArea;
        public Slider goodSlider;
        public Slider badSlider;
        public Transform handle;

        void Awake() {
            _instance = this;
        }

        void Start() {
            willingness = PlayerPrefs.GetFloat("willingness", 0);
            UpdateUI();
        }

        public void AddWillingness(float value) {
            _instance.ChangeWillingness(_instance.willingness + value);
        }

        public void ChangeWillingness(float newValue) {
            willingness = Mathf.Clamp(newValue, -1f, 1f);
            PlayerPrefs.SetFloat("willingness", willingness);
            UpdateUI();
        }

        void UpdateUI() {
            if (badSlider == null || goodSlider == null || handle == null || sliderArea == null) {
                Debug.LogWarning("There are some null values here", gameObject);
                return;
            }

            if (willingness < 0) {
                badSlider.value = -willingness;
                goodSlider.value = 0;
            } else if (willingness > 0) {
                badSlider.value = 0;
                goodSlider.value = willingness;
            } else {
                badSlider.value = 0;
                goodSlider.value = 0;
            }

            var pos = handle.localPosition;
            pos.x = Mathf.Lerp(sliderArea.rect.xMin, sliderArea.rect.xMax, (1 + willingness) / 2f);
            handle.localPosition = pos;
        }
    }
}