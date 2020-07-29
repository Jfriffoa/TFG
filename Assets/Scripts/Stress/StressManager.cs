using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

namespace TFG.Stress {
    public class StressManager : MonoBehaviour {
        // Singleton
        static StressManager _instance;
        public static StressManager Instance { get => _instance; }

        [Header("Game")]
        public SpawnOnUI spawner;

        [Header("UI")]
        public Slider stressBar;
        float _stress;

        public TextMeshProUGUI timer;
        public float initialTime = 30;
        float _startTime;

        public TextMeshProUGUI postGameText;

        [Header("Stress Effects")]
        public Image inverted;
        public Image mask;

        [Header("Callbacks")]
        public UnityEvent onTimeEnds;

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
            // Rest 5% of stress each second and Update Stress Bar
            _stress = Mathf.Clamp01(_stress - 0.05f * Time.deltaTime);
            stressBar.value = _stress;

            // Update Time
            var time = initialTime - Mathf.Ceil(Time.time - _startTime);
            timer.text = time + "";

            // Effects
            inverted.color = new Color(inverted.color.r, inverted.color.g, inverted.color.b, _stress);
            mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, (_stress - .25f)/ .75f);

            // Apply Difficult Change
            if (time < 20) {
                spawner.timeToSpawn = Mathf.Lerp(.5f, 2f, (time - 5) / 15);
            }

            // Check if Time is over
                if (time <= 0) {
                Debug.Log("Time Ended");
                Time.timeScale = 0;
                onTimeEnds.Invoke();

                PlayerPrefs.SetInt("Stress", Mathf.RoundToInt(_stress));
                var isStressed = _stress >= 0.5f;

                if (isStressed) {
                    postGameText.text = "Estás Estresada...";
                } else {
                    postGameText.text = "Debes entregar la prueba...";
                }
            }
        }
    }
}