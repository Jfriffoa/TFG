using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG {
    public class FollowObject : MonoBehaviour {
        public Transform toFollow;
        public Vector3 offset;

        void LateUpdate() {
            transform.position = toFollow.position + offset;
        }
    }
}