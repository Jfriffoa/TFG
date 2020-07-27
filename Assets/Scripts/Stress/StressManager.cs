using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace TFG.Stress {
    public class StressManager : MonoBehaviour {
        // Singleton
        static StressManager _instance;
        public static StressManager Instance { get => _instance; }

        //[Header("UI")]
        public Slider stressBar;
        float _stress;

        public TextMeshProUGUI timer;
        public float initialTime = 30;
        float _startTime;


        internal void AddStress(float amount) {
            _stress += amount;
        }

        void Awake() {
            if (_instance != null) {
                Debug.LogWarning("There's another instance of the Manager. Deleting this one");
                Destroy(gameObject);
            } else {
                _instance = this;
            }
        }

        void Start() {
            _startTime = Time.time;
        }

        void LateUpdate() {
            // Rest 5% of stress each second
            _stress = Mathf.Clamp01(_stress - 0.05f * Time.deltaTime);
            // Update UI
            stressBar.value = _stress;

            var time = initialTime - Mathf.Ceil(Time.time - _startTime);
            timer.text = time + "";

            if (time <= 0) {
                Debug.Log("Time Ended");
                Time.timeScale = 0;
            }
        }
    }
}