using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace TFG {
    public class DelayEvent : MonoBehaviour {
        public bool onStart;
        public float delay;
        public UnityEvent _event;

        void Start() {
            if (onStart) {
                Invoke();
            }
        }

        public void Invoke() {
            Invoke("DoEvent", delay);
        }

        public void InvokeImmediatly() {
            DoEvent();
        }

        void DoEvent() {
            _event.Invoke();
        }
    }
}