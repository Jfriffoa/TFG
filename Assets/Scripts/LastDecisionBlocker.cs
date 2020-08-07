using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace TFG {
    public class LastDecisionBlocker : MonoBehaviour {

        [Range(0f, 1f)]
        public float threshold = 0.75f;
        public Button vive;
        public Button muere;

        void Start() {
            vive.interactable = WillingnessController.Instance.willingness >= -threshold;
            muere.interactable = WillingnessController.Instance.willingness <= threshold;
        }
    }
}