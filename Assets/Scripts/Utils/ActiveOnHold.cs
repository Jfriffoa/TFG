using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace TFG {
    public class ActiveOnHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        public GameObject[] toActive;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData) {
            foreach (var go in toActive) {
                go.SetActive(true);
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
            foreach (var go in toActive) {
                go.SetActive(false);
            }
        }
    }
}