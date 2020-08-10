using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

namespace TFG.Relax {
    public class RelaxController : MonoBehaviour {
        [Header("Game")]
        public int leafGoal = 2;
        public float stressStep = 0.05f;
        GameLine[] _lines;

        [Header("UI")]
        public Slider stressBar;
        float _stress;

        public TextMeshProUGUI timer;
        public float initialTime = 30;
        float _startTime;

        [Header("Callbacks")]
        public UnityEvent onEnd;

        // Start is called before the first frame update
        void Start() {
            _lines = GetComponentsInChildren<GameLine>();
            _startTime = Time.time;
        }

        void Update() {
            // Update Bar
            var newVal = _stress + stressStep * (CheckLines() ? -1 : 1) * Time.deltaTime;
            _stress = Mathf.Clamp01(newVal);
            stressBar.value = _stress;

            // Check Time
            var time = initialTime - Mathf.Ceil(Time.time - _startTime);
            timer.text = time + "";

            if (time <= 0) {
                Debug.Log("Time Ended");
                onEnd.Invoke();
                Time.timeScale = 0;
            }

            // Relax Lucia
            if (_stress <= 0.25f) {
                WillingnessController.Instance.AddWillingness(0.01f * Time.deltaTime);
            }
        }

        bool CheckLines() {
            foreach (var line in _lines) {
                if (line.Count != leafGoal)
                    return false;
            }

            return true;
        }
    }
}