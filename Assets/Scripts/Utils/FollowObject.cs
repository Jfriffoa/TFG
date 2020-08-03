using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG {
    public class FollowObject : MonoBehaviour {
        public Transform toFollow;
        public Vector3 offset;
        public Rect bounds;

        public bool pixelPerfect;

        void Update() {
            if (toFollow != null && bounds.Contains(toFollow.position + offset)) {
                if (!pixelPerfect)
                    transform.position = toFollow.position + offset;
                else {
                    var x = toFollow.position.x + offset.x;
                    var y = toFollow.position.y + offset.y;

                    var roundedX = RoundToNearestPixel(x);
                    var roundedY = RoundToNearestPixel(y);

                    transform.position = new Vector3(roundedX, roundedY, transform.position.z);
                }
            }
        }

        public float pixelToUnits = 56f;
        float RoundToNearestPixel(float unityUnits) {
            float valueInPixels = unityUnits * pixelToUnits;
            valueInPixels = Mathf.Round(valueInPixels);
            return valueInPixels * (1 / pixelToUnits);
        }
    }
}