using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TFG.Stress {
    [System.Serializable] public class FloatEvent : UnityEvent<float> {}
    [System.Serializable] public class ColorEvent : UnityEvent<Color> {}

    public class StressUIController : MonoBehaviour {
        public Animator[] stressAnimators;

        public UnityEvent onStressed;
        public FloatEvent onStressLevelChanged;

        // Start is called before the first frame update
        void Start() {
            if (PlayerPrefs.GetInt("Stress", 0) == 1) {
                onStressed.Invoke();
            }
        }

        public void StressLevel(float level) {
            onStressLevelChanged.Invoke(level);
            foreach (var ani in stressAnimators) {
                ani.SetFloat("Stress Level", level);
            }
        }
    }
}