using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG {
    public class ResetTimeScale : MonoBehaviour {
        void Start() {
            ResetTime();
        }

        public void ResetTime() {
            Time.timeScale = 1;
        }

        public void PauseTime() {
            Time.timeScale = 0;
        }
    }
}