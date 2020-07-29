using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG {
    public class FollowObject : MonoBehaviour {
        public Transform toFollow;
        public Vector3 offset;
        public Rect bounds;

        void LateUpdate() {
            if (toFollow != null && bounds.Contains(toFollow.position + offset)) {
                transform.position = toFollow.position + offset;
            }
        }
    }
}